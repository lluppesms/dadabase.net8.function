// --------------------------------------------------------------------------------
// This BICEP file will create a KeyVault
// FYI: To purge a KV with soft delete enabled: > az keyvault purge --name kvName
// --------------------------------------------------------------------------------
param keyVaultName string = 'mykeyvaultname'
param location string = resourceGroup().location
param commonTags object = {}

@description('Administrators that should have access to administer key vault')
param adminUserObjectIds array = []
@description('Application that should have access to read key vault secrets')
param applicationUserObjectIds array = []

@description('Administrator UserId that should have access to administer key vault')
param keyVaultOwnerUserId string = ''
@description('Ip Address of the KV owner so they can read the vault, such as 254.254.254.254/32')
param keyVaultOwnerIpAddress string = ''

@description('Determines if Azure can deploy certificates from this Key Vault.')
param enabledForDeployment bool = true
@description('Determines if templates can reference secrets from this Key Vault.')
param enabledForTemplateDeployment bool = true
@description('Determines if this Key Vault can be used for Azure Disk Encryption.')
param enabledForDiskEncryption bool = true
@description('Determine if soft delete is enabled on this Key Vault.')
param enableSoftDelete bool = false
@description('Determine if purge protection is enabled on this Key Vault.')
param enablePurgeProtection bool = true
@description('The number of days to retain soft deleted vaults and vault objects.')
param softDeleteRetentionInDays int = 7
@description('Determines if access to the objects granted using RBAC. When true, access policies are ignored.')
param useRBAC bool = false

@allowed(['Enabled','Disabled'])
param publicNetworkAccess string = 'Enabled'
@allowed(['Allow','Deny'])
param allowNetworkAccess string = 'Allow'

@description('The user assigned identity name that will be granted access to secrets')
param managedIdentityName string = ''
@description('The user assigned identity principal Id that will be granted access to secrets')
param managedIdentityPrincipalId string = ''
@description('The user assigned identity tenant Id that will be granted access to secrets')
param managedIdentityTenantId string = ''

@description('The DAPR identity name that will be granted access to secrets')
param daprIdentityName string = ''
@description('The DAPR identity principal Id that will be granted access to secrets')
param daprIdentityPrincipalId string = ''
@description('The DAPR tenant Id that will be granted access to secrets')
param daprIdentityTenantId string = ''

@description('The workspace to store audit logs.')
@metadata({
  strongType: 'Microsoft.OperationalInsights/workspaces'
  example: '/subscriptions/<subscription_id>/resourceGroups/<resource_group>/providers/Microsoft.OperationalInsights/workspaces/<workspace_name>'
})
param workspaceId string = ''

// --------------------------------------------------------------------------------
var templateTag = { TemplateFile: '~keyvault.bicep' }
var tags = union(commonTags, templateTag)

var skuName = 'standard'
var subTenantId = subscription().tenantId

var ownerAccessPolicy = keyVaultOwnerUserId == '' ? [] : [
  {
    objectId: keyVaultOwnerUserId
    tenantId: subTenantId
    permissions: {
      certificates: [ 'all' ]
      secrets: [ 'all' ]
      keys: [ 'all' ]
    }
  } 
]
var adminAccessPolicies = [for adminUser in adminUserObjectIds: {
  objectId: adminUser
  tenantId: subTenantId
  permissions: {
    certificates: [ 'all' ]
    secrets: [ 'all' ]
    keys: [ 'all' ]
  }
}]
var applicationUserPolicies = [for appUser in applicationUserObjectIds: {
  objectId: appUser
  tenantId: subTenantId
  permissions: {
    secrets: [ 'get' ]
    keys: [ 'get', 'wrapKey', 'unwrapKey' ] // Azure SQL uses these permissions to access TDE key
  }
}]
var accessPolicies = union(ownerAccessPolicy, adminAccessPolicies, applicationUserPolicies)

var kvIpRules = keyVaultOwnerIpAddress == '' ? [] : [
  {
    value: keyVaultOwnerIpAddress
  }
] 

// --------------------------------------------------------------------------------
resource keyVaultResource 'Microsoft.KeyVault/vaults@2023-07-01' = {
  name: keyVaultName
  location: location
  tags: tags
  properties: {
    sku: {
      family: 'A'
      name: skuName
    }
    tenantId: subTenantId
    enableRbacAuthorization: useRBAC
    accessPolicies: accessPolicies
    enabledForDeployment: enabledForDeployment
    enabledForDiskEncryption: enabledForDiskEncryption
    enabledForTemplateDeployment: enabledForTemplateDeployment
    enableSoftDelete: enableSoftDelete
    enablePurgeProtection: enablePurgeProtection // Not allowing to purge key vault or its objects after deletion
    createMode: 'default'                        // Creating or updating the key vault (not recovering)
    softDeleteRetentionInDays: softDeleteRetentionInDays
    publicNetworkAccess: publicNetworkAccess   // Allow access from all networks
    networkAcls: {
      defaultAction: allowNetworkAccess
      bypass: 'AzureServices'
      ipRules: kvIpRules
      virtualNetworkRules: []
    }
  }
}

// this will grant access to an application identity that can be used to get secrets
var grantManagedIdentityAccess = !empty(managedIdentityPrincipalId)
resource existingManagedIdentity 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-07-31-preview' existing = if (grantManagedIdentityAccess) {
  name: managedIdentityName
}
var userAssignedIdentityPolicies = (!grantManagedIdentityAccess) ? [] : [{
  tenantId: managedIdentityTenantId
  objectId: managedIdentityPrincipalId
  permissions: {
    secrets: ['get','list','set']
  }
}]

// this will grant access to an identity for DAPR that can be used to get secrets
var grantDAPRIdentityAccess = !empty(daprIdentityTenantId)
resource existingDaprIdentity 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-07-31-preview' existing = if (grantDAPRIdentityAccess) {
  name: daprIdentityName
}
var daprIdentityPolicies = (!grantDAPRIdentityAccess) ? [] : [{
  tenantId: daprIdentityTenantId
  objectId: daprIdentityPrincipalId
  permissions: {
    secrets: ['get','list']
  }
}]

// you can only do one add Policy in a Bicep file, so we union the policies together
var userIdentityPolicies = union(userAssignedIdentityPolicies, daprIdentityPolicies)

resource userAssignedIdentityKeyVaultAccessPolicy 'Microsoft.KeyVault/vaults/accessPolicies@2023-07-01' = if (!useRBAC && (grantManagedIdentityAccess || grantDAPRIdentityAccess)) {
  name: 'add'
  parent: keyVaultResource
  properties: {
    accessPolicies: userIdentityPolicies
  }
}

resource keyVaultAuditLogging 'Microsoft.Insights/diagnosticSettings@2021-05-01-preview' = if (workspaceId != '') {
  name: '${keyVaultResource.name}-auditlogs'
  scope: keyVaultResource
  properties: {
    workspaceId: workspaceId
    logs: [
      {
        category: 'AuditEvent'
        enabled: true
        // Note: Causes error: Diagnostic settings does not support retention for new diagnostic settings.
        // retentionPolicy: {
        //   days: 180
        //   enabled: true 
        // }
      }
    ]
  }
}

resource keyVaultMetricLogging 'Microsoft.Insights/diagnosticSettings@2021-05-01-preview' = if (workspaceId != '') {
  name: '${keyVaultResource.name}-metrics'
  scope: keyVaultResource
  properties: {
    workspaceId: workspaceId
    metrics: [
      {
        category: 'AllMetrics'
        enabled: true
        // Note: Causes error: Diagnostic settings does not support retention for new diagnostic settings.
        // retentionPolicy: {
        //   days: 30
        //   enabled: true 
        // }
      }
    ]
  }
}

// --------------------------------------------------------------------------------
output name string = keyVaultResource.name
output id string = keyVaultResource.id
output userManagedIdentityId string = grantManagedIdentityAccess ? existingManagedIdentity.id : ''
output daprManagedIdentityId string = grantDAPRIdentityAccess ? existingDaprIdentity.id : ''
