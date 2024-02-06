// ----------------------------------------------------------------------------------------------------
// This BICEP file is the main entry point for the azd command
// NOTE: the infrastructure deploys fine, but the .NET 7 Function does not appear to deploy properly
//   ... not sure if this is a project issue or if the AZD command just doesn't support an
//       isolated function...????
// ----------------------------------------------------------------------------------------------------
param name string = ''
param location string = ''
param runDateTime string = utcNow()
// param principalId string = ''

// --------------------------------------------------------------------------------
targetScope = 'subscription'

// --------------------------------------------------------------------------------
var tags = {
    Application: name
    LastDeployed: runDateTime
}
var deploymentSuffix = '-${runDateTime}'

// --------------------------------------------------------------------------------
resource resourceGroup 'Microsoft.Resources/resourceGroups@2020-06-01' = {
    name: 'rg-${name}'
    location: location
    tags: tags
}

module resources './Bicep/main.bicep' = {
    name: 'resources-${deploymentSuffix}'
    scope: resourceGroup
    params: {
        location: location
        appName: name
        environmentCode: 'azd'
    }
}
