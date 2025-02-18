// ----------------------------------------------------------------------------------------------------
// This BICEP file is the main entry point for the azd command
// NOTE: the infrastructure deploys fine, but the .NET 7 Function does not appear to deploy properly
//   ... not sure if this is a project issue or if the AZD command just doesn't support an
//       isolated function...????
// ----------------------------------------------------------------------------------------------------
param appName string = ''
param location string = ''
param runDateTime string = utcNow()
param adminUserId string = ''

// --------------------------------------------------------------------------------
targetScope = 'subscription'

// --------------------------------------------------------------------------------
var tags = {
    Application: appName
    LastDeployed: runDateTime
}
var deploymentSuffix = '-${runDateTime}'

// --------------------------------------------------------------------------------
resource resourceGroup 'Microsoft.Resources/resourceGroups@2020-06-01' = {
    name: 'rg-${appName}'
    location: location
    tags: tags
}

module resources './Bicep/main.bicep' = {
    name: 'resources-${deploymentSuffix}'
    scope: resourceGroup
    params: {
        appName: appName
        location: location
        environmentCode: 'azd'
        storageSku: 'Standard_LRS'
        functionAppSku: 'Y1'
        functionAppSkuFamily: 'Y'
        functionAppSkuTier: 'Dynamic'
        adminUserId: adminUserId
    }
}
