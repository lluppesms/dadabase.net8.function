// --------------------------------------------------------------------------------
// Bicep file that builds all the resource names used by other Bicep templates
// --------------------------------------------------------------------------------
param appName string = ''
@allowed(['azd','gha','azdo','dev','demo','qa','stg','ct','prod'])
param environmentCode string = 'azd'

param functionStorageNameSuffix string = 'func'
param dataStorageNameSuffix string = 'data'
param environmentSpecificFunctionName string = ''

// --------------------------------------------------------------------------------
// pull resource abbreviations from a common JSON file
var resourceAbbreviations = loadJsonContent('./data/resourceAbbreviations.json')

// --------------------------------------------------------------------------------
var lowerAppName = replace(toLower(appName), ' ', '')
var sanitizedAppName = replace(replace(lowerAppName, '-', ''), '_', '')
var sanitizedEnvironment = toLower(environmentCode)

// --------------------------------------------------------------------------------
// if there's an environment specific function name specified, use that, otherwise if it's azd -- 
// other resource names can be changed if desired, but if using the "azd deploy" command it expects the
// function name to be exactly "{appName}function" so don't change the functionAppName format if using azd
var functionAppName = environmentSpecificFunctionName == '' ? environmentCode == 'azd' ? '${lowerAppName}function' : toLower('${lowerAppName}-${sanitizedEnvironment}') : environmentSpecificFunctionName
var baseStorageName = toLower('${sanitizedAppName}${sanitizedEnvironment}str')

// --------------------------------------------------------------------------------
output userAssignedIdentityName string = toLower('${sanitizedAppName}-${sanitizedEnvironment}-${resourceAbbreviations.identitySuffix}')
output logAnalyticsWorkspaceName string =  toLower('${lowerAppName}-${sanitizedEnvironment}-${resourceAbbreviations.logWorkspaceSuffix}')
output functionAppName string            = functionAppName
output functionAppServicePlanName string = '${functionAppName}-${resourceAbbreviations.appServicePlanSuffix}'
output functionInsightsName string       = '${functionAppName}-${resourceAbbreviations.appInsightsSuffix}'

output cosmosAccountName string =          toLower('${sanitizedAppName}-${resourceAbbreviations.cosmosDatabase}-${sanitizedEnvironment}')
output serviceBusName string =             toLower('${sanitizedAppName}-${resourceAbbreviations.serviceBus}-${sanitizedEnvironment}')

// Key Vaults and Storage Accounts can only be 24 characters long
output keyVaultName string               = take(toLower('${sanitizedAppName}${sanitizedEnvironment}${resourceAbbreviations.keyVaultAbbreviation}'), 24)
output functionStorageName string        = take('${baseStorageName}${functionStorageNameSuffix}', 24)
output dataStorageName string            = take('${baseStorageName}${dataStorageNameSuffix}', 24)
// output functionStorageName string        = take('${baseStorageName}${functionStorageNameSuffix}${uniqueString(resourceGroup().id)}', 24)
// output dataStorageName string            = take('${baseStorageName}${dataStorageNameSuffix}${uniqueString(resourceGroup().id)}', 24)
