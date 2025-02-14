// --------------------------------------------------------------------------------
// Main Bicep file that creates all of the Azure Resources for one environment
// --------------------------------------------------------------------------------
param appName string = ''
param environmentCode string = 'azd'
param location string = resourceGroup().location
//param keyVaultOwnerUserId string = ''

param environmentSpecificFunctionName string = ''

// optional parameters
@allowed(['Standard_LRS','Standard_GRS','Standard_RAGRS'])
param storageSku string = 'Standard_LRS'
param functionAppSku string = 'Y1'
param functionAppSkuFamily string = 'Y'
param functionAppSkuTier string = 'Dynamic'

@description('Add Role Assignments for the user assigned identity?')
param addRoleAssignments bool = true

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
module logAnalyticsWorkspaceModule 'loganalyticsworkspace.bicep' = {
  name: 'logAnalytics${deploymentSuffix}'
  params: {
    logAnalyticsWorkspaceName: resourceNames.outputs.logAnalyticsWorkspaceName
    location: location
    commonTags: commonTags
  }
}

// --------------------------------------------------------------------------------
module identity 'identity.bicep' = {
  name: 'app-identity${deploymentSuffix}'
  params: {
    identityName: resourceNames.outputs.userAssignedIdentityName
    location: location
  }
}
module roleAssignments 'role-assignments.bicep' = if (addRoleAssignments) {
  name: 'identity-access${deploymentSuffix}'
  params: {
    storageAccountName: functionStorageModule.outputs.name
    identityPrincipalId: identity.outputs.managedIdentityPrincipalId
  }
}

// --------------------------------------------------------------------------------
module functionStorageModule 'storageaccount.bicep' = {
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

module functionModule 'functionapp.bicep' = {
  name: 'function${deploymentSuffix}'
  dependsOn: [ roleAssignments ]
  params: {
    functionAppName: resourceNames.outputs.functionAppName
    functionAppServicePlanName: resourceNames.outputs.functionAppServicePlanName
    functionInsightsName: resourceNames.outputs.functionInsightsName

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

module functionAppSettingsModule 'functionappsettings.bicep' = {
  name: 'functionAppSettings${deploymentSuffix}'
  params: {
    functionAppName: functionModule.outputs.name
    functionStorageAccountName: functionModule.outputs.storageAccountName
    functionInsightsKey: functionModule.outputs.insightsKey
    customAppSettings: {
      OpenApi__HideSwaggerUI: 'false'
      OpenApi__HideDocument: 'false'
      OpenApi__DocTitle: 'Isolated .NET8 Functions Demo APIs'
      OpenApi__DocDescription: 'This repo is an example of how to use Isolated .NET8 Azure Functions'
    }
  }
}

output SUBSCRIPTION_ID string = subscription().subscriptionId
output RESOURCE_GROUP_NAME string = resourceGroupName
output HOST_NAME string = functionModule.outputs.hostname
