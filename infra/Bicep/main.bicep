// --------------------------------------------------------------------------------
// Main Bicep file that creates all of the Azure Resources for one environment
// --------------------------------------------------------------------------------
param appName string
param environmentCode string = 'azd'
param location string = resourceGroup().location

// optional parameters
param environmentSpecificFunctionName string = ''
// @allowed(['Standard_LRS','Standard_GRS','Standard_RAGRS'])
param storageSku string = 'Standard_LRS'
param functionAppSku string = 'Y1'
param functionAppSkuFamily string = 'Y'
param functionAppSkuTier string = 'Dynamic'

@description('Add Role Assignments for the user assigned identity?')
param addRoleAssignments bool = true

@description('Run script to deplicate secrets?')
param deDuplicateSecrets bool = true

@description('Add this Admin User Id to KeyVault Access')
param adminUserId string = ''

// calculated variables disguised as parameters
param runDateTime string = utcNow()

// --------------------------------------------------------------------------------
var deploymentSuffix = '-${runDateTime}'
var commonTags = {         
  LastDeployed: runDateTime
  Application: appName
  Environment: environmentCode
}
var resourceGroupName = resourceGroup().name

// --------------------------------------------------------------------------------
module resourceNames 'resourcenames.bicep' = {
  name: 'resourcenames${deploymentSuffix}'
  params: {
    appName: appName
    environmentCode: environmentCode
    functionStorageNameSuffix: 'func'
    dataStorageNameSuffix: 'data'
    environmentSpecificFunctionName: environmentSpecificFunctionName
  }
}

// --------------------------------------------------------------------------------
module logAnalyticsWorkspaceModule './app/loganalyticsworkspace.bicep' = {
  name: 'logAnalytics${deploymentSuffix}'
  params: {
    logAnalyticsWorkspaceName: resourceNames.outputs.logAnalyticsWorkspaceName
    location: location
    commonTags: commonTags
  }
}

// --------------------------------------------------------------------------------
module functionStorageModule './app/storageaccount.bicep' = {
  name: 'functionstorage${deploymentSuffix}'
  params: {
    storageSku: storageSku
    storageAccountName: resourceNames.outputs.functionStorageName
    location: location
    commonTags: commonTags
    allowNetworkAccess: 'Allow'
    publicNetworkAccess: 'Enabled'
  }
}

// --------------------------------------------------------------------------------
module identity './security/identity.bicep' = {
  name: 'appIdentity${deploymentSuffix}'
  params: {
    identityName: resourceNames.outputs.userAssignedIdentityName
    location: location
  }
}
module appRoleAssignments './security/roleassignments.bicep' = if (addRoleAssignments) {
  name: 'appRoleAssignments${deploymentSuffix}'
  params: {
    identityPrincipalId: identity.outputs.managedIdentityPrincipalId
    principalType: 'ServicePrincipal'
    storageAccountName: functionStorageModule.outputs.name
    keyVaultName:  keyVaultModule.outputs.name
  }
}
module adminRoleAssignments './security/roleassignments.bicep' = if (addRoleAssignments) {
  name: 'userRoleAssignments${deploymentSuffix}'
  params: {
    identityPrincipalId: adminUserId
    principalType: 'User'
    storageAccountName: functionStorageModule.outputs.name
    keyVaultName:  keyVaultModule.outputs.name
  }
}


// --------------------------------------------------------------------------------
module keyVaultModule './security/keyvault.bicep' = {
  name: 'keyVault${deploymentSuffix}'
  params: {
    keyVaultName: resourceNames.outputs.keyVaultName
    location: location
    commonTags: commonTags
    adminUserObjectIds: [ adminUserId ]
    applicationUserObjectIds: [ ]
    workspaceId: logAnalyticsWorkspaceModule.outputs.id
    managedIdentityPrincipalId: identity.outputs.managedIdentityPrincipalId
    managedIdentityTenantId: identity.outputs.managedIdentityTenantId
    publicNetworkAccess: 'Disabled'
    allowNetworkAccess: 'Allow'
    useRBAC: true
  }
}

module keyVaultSecretList './security/keyvaultlistsecretnames.bicep' = if (deDuplicateSecrets) {
  name: 'keyVaultSecretListNames${deploymentSuffix}'
  params: {
    keyVaultName: keyVaultModule.outputs.name
    location: location
    userManagedIdentityId: keyVaultModule.outputs.userManagedIdentityId
  }
}
module keyVaultStorageSecret './security/keyvaultsecretstorageconnection.bicep' = {
  name: 'keyVaultStorageSecret${deploymentSuffix}'
  params: {
    keyVaultName: keyVaultModule.outputs.name
    secretName: 'azurefilesconnectionstring'
    storageAccountName: functionStorageModule.outputs.name
    existingSecretNames: deDuplicateSecrets ? keyVaultSecretList.outputs.secretNameList : ''
  }
}

// --------------------------------------------------------------------------------
module functionModule './app/functionapp.bicep' = {
  name: 'function${deploymentSuffix}'
  dependsOn: [ appRoleAssignments ]
  params: {
    functionAppName: resourceNames.outputs.functionAppName
    functionAppServicePlanName: resourceNames.outputs.functionAppServicePlanName
    functionInsightsName: resourceNames.outputs.functionInsightsName
    managedIdentityId: identity.outputs.managedIdentityId
    keyVaultName: keyVaultModule.outputs.name

    appInsightsLocation: location
    location: location
    commonTags: commonTags

    functionKind: 'functionapp,linux'
    functionAppSku: functionAppSku
    functionAppSkuFamily: functionAppSkuFamily
    functionAppSkuTier: functionAppSkuTier
    functionStorageAccountName: functionStorageModule.outputs.name
    workspaceId: logAnalyticsWorkspaceModule.outputs.id
  }
}

module functionAppSettingsModule './app/functionappsettings.bicep' = {
  name: 'functionAppSettings${deploymentSuffix}'
  params: {
    functionAppName: functionModule.outputs.name
    functionStorageAccountName: functionModule.outputs.storageAccountName
    functionInsightsKey: functionModule.outputs.insightsKey
    keyVaultName: keyVaultModule.outputs.name
    customAppSettings: {
      OpenApi__HideSwaggerUI: 'false'
      OpenApi__HideDocument: 'false'
      OpenApi__DocTitle: 'Isolated .NET8 Functions Demo APIs'
      OpenApi__DocDescription: 'This repo is an example of how to use Isolated .NET8 Azure Functions'
    }
  }
}

// --------------------------------------------------------------------------------
output SUBSCRIPTION_ID string = subscription().subscriptionId
output RESOURCE_GROUP_NAME string = resourceGroupName
output HOST_NAME string = functionModule.outputs.hostname
