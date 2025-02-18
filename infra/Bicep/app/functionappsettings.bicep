// --------------------------------------------------------------------------------
// This BICEP file will add unique Configuration settings to a web or function app
// --------------------------------------------------------------------------------
param functionAppName string
param functionStorageAccountName string
param functionInsightsKey string
param customAppSettings object = {}
param keyVaultName string
param functionsWorkerRuntime string = 'DOTNET-ISOLATED'
param functionsExtensionVersion string = '~4'
param nodeDefaultVersion string = '8.11.1'
param use32BitProcess string = 'false'
param netFrameworkVersion string = 'v8.0'
param usePlaceholderDotNetIsolated string = '1'

var useKeyVaultConnection = false

resource storageAccountResource 'Microsoft.Storage/storageAccounts@2019-06-01' existing = { 
  name: functionStorageAccountName 
}
var accountKey = storageAccountResource.listKeys().keys[0].value
var storageAccountConnectionString = 'DefaultEndpointsProtocol=https;AccountName=${storageAccountResource.name};EndpointSuffix=${environment().suffixes.storage};AccountKey=${accountKey}'
var functionStorageAccountKeyVaultReference = '@Microsoft.KeyVault(VaultName=${keyVaultName};SecretName=azurefilesconnectionstring)'

var BASE_SLOT_APPSETTINGS = {
  // See https://learn.microsoft.com/en-us/azure/azure-functions/functions-identity-based-connections-tutorial
  AzureWebJobsStorage: useKeyVaultConnection ? functionStorageAccountKeyVaultReference : storageAccountConnectionString
  AzureWebJobsStorage__accountName: functionStorageAccountName
  AzureWebJobsDashboard: useKeyVaultConnection ? functionStorageAccountKeyVaultReference : storageAccountConnectionString
  WEBSITE_CONTENTAZUREFILECONNECTIONSTRING: useKeyVaultConnection ? functionStorageAccountKeyVaultReference : storageAccountConnectionString
  WEBSITE_CONTENTSHARE: functionAppName
  APPINSIGHTS_INSTRUMENTATIONKEY: functionInsightsKey
  APPLICATIONINSIGHTS_CONNECTION_STRING: 'InstrumentationKey=${functionInsightsKey}'

  FUNCTIONS_WORKER_RUNTIME: functionsWorkerRuntime
  FUNCTIONS_EXTENSION_VERSION: functionsExtensionVersion
  WEBSITE_NODE_DEFAULT_VERSION: nodeDefaultVersion
  USE32BITWORKERPROCESS: use32BitProcess
  NET_FRAMEWORK_VERSION: netFrameworkVersion
  WEBSITE_USE_PLACEHOLDER_DOTNETISOLATED: usePlaceholderDotNetIsolated
}

// This *should* work, but I keep getting a "circular dependency detected" error and it doesn't work
// resource appResource 'Microsoft.Web/sites@2021-03-01' existing = { name: functionAppName }
// var BASE_SLOT_APPSETTINGS = list('${appResource.id}/config/appsettings', appResource.apiVersion).properties

resource functionApp 'Microsoft.Web/sites@2021-02-01' existing = {
  name: functionAppName
}

resource siteConfig 'Microsoft.Web/sites/config@2021-02-01' = {
  name: 'appsettings'
  parent: functionApp
  properties: union(BASE_SLOT_APPSETTINGS, customAppSettings)
}
