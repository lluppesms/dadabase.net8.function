// ----------------------------------------------------------------------------------------------------
// This BICEP file will create an .NET 8 Isolated Azure Function
// Changed to use Managed Identity Storage (no connection string)
// ----------------------------------------------------------------------------------------------------
param functionAppName string
param functionAppServicePlanName string
param functionInsightsName string
param functionStorageAccountName string

param location string = resourceGroup().location
param appInsightsLocation string = resourceGroup().location
param commonTags object = {}

param managedIdentityId string
param keyVaultName string

@allowed([ 'functionapp', 'functionapp,linux' ])
param functionKind string = 'functionapp,linux'
param functionHostKind string = 'linux'
param functionAppSku string = 'Y1'
param functionAppSkuFamily string = 'Y'
param functionAppSkuTier string = 'Dynamic'
param linuxFxVersion string = 'DOTNET-ISOLATED|8.0'

param functionsWorkerRuntime string = 'DOTNET-ISOLATED'
param functionsExtensionVersion string = '~4'
param nodeDefaultVersion string = '8.11.1'
param use32BitProcess string = 'false'
param netFrameworkVersion string = 'v4.0'
param usePlaceholderDotNetIsolated string = '1'

param workerSizeId int = 0
param numberOfWorkers int = 1
param maximumWorkerCount int = 1

param publicNetworkAccess string = 'Enabled'

@description('The workspace to store audit logs.')
param workspaceId string = ''

// --------------------------------------------------------------------------------
var templateTag = { TemplateFile: '~functionapp.bicep' }
var azdTag = { 'azd-service-name': 'function' }
var tags = union(commonTags, templateTag)
var functionTags = union(commonTags, templateTag, azdTag)
var useKeyVaultConnection = false

// --------------------------------------------------------------------------------
resource storageAccountResource 'Microsoft.Storage/storageAccounts@2019-06-01' existing = { name: functionStorageAccountName }
var accountKey = storageAccountResource.listKeys().keys[0].value
var functionStorageAccountConnectionString = 'DefaultEndpointsProtocol=https;AccountName=${storageAccountResource.name};EndpointSuffix=${environment().suffixes.storage};AccountKey=${accountKey}'
var functionStorageAccountKeyVaultReference = '@Microsoft.KeyVault(VaultName=${keyVaultName};SecretName=azurefilesconnectionstring)'

resource appInsightsResource 'Microsoft.Insights/components@2020-02-02-preview' = {
  name: functionInsightsName
  location: appInsightsLocation
  kind: 'web'
  tags: tags
  properties: {
    Application_Type: 'web'
    Request_Source: 'rest'
    publicNetworkAccessForIngestion: 'Enabled'
    publicNetworkAccessForQuery: 'Enabled'
    WorkspaceResourceId: workspaceId
  }
}

resource appServiceResource 'Microsoft.Web/serverfarms@2021-03-01' = {
  name: functionAppServicePlanName
  location: location
  kind: functionHostKind
  tags: tags
  sku: {
    name: functionAppSku
    tier: functionAppSkuTier
    size: functionAppSku
    family: functionAppSkuFamily
    capacity: 0
  }
  properties: {
    perSiteScaling: false
    elasticScaleEnabled: false
    maximumElasticWorkerCount: maximumWorkerCount
    targetWorkerCount: workerSizeId
    targetWorkerSizeId: numberOfWorkers
    isSpot: false
    reserved: true
    isXenon: false
    hyperV: false
    zoneRedundant: false
  }
}

resource functionAppResource 'Microsoft.Web/sites@2023-01-01' = {
  name: functionAppName
  location: location
  kind: functionKind
  tags: functionTags
  identity: {
    //disable-next-line BCP036
    type: 'SystemAssigned, UserAssigned'
    //disable-next-line BCP036
    userAssignedIdentities: { '${managedIdentityId}': {} }
  }
  properties: {
    enabled: true
    serverFarmId: appServiceResource.id
    reserved: true
    isXenon: false
    hyperV: false
    vnetRouteAllEnabled: false
    vnetImagePullEnabled: false
    vnetContentShareEnabled: false
    siteConfig: {
      numberOfWorkers: numberOfWorkers
      linuxFxVersion: linuxFxVersion
      acrUseManagedIdentityCreds: false
      alwaysOn: false
      http20Enabled: false
      functionAppScaleLimit: 200
      minimumElasticInstanceCount: 0
      ftpsState: 'FtpsOnly'
      minTlsVersion: '1.2'
      appSettings: [
        // See https://learn.microsoft.com/en-us/azure/azure-functions/functions-identity-based-connections-tutorial
        // {
        //   name: 'AzureWebJobsStorage'
        //   value: functionStorageAccountConnectionString
        // }
        {
          name: 'AzureWebJobsStorage__accountName'
          value: functionStorageAccountName
        }
        {
          name: 'AzureWebJobsDashboard'
          value: useKeyVaultConnection ? functionStorageAccountKeyVaultReference : functionStorageAccountConnectionString
        }
        {
          name: 'WEBSITE_CONTENTAZUREFILECONNECTIONSTRING'
          value: useKeyVaultConnection ? functionStorageAccountKeyVaultReference : functionStorageAccountConnectionString
        }
        {
          name: 'StorageAccountConnectionString'
          value: useKeyVaultConnection ? functionStorageAccountKeyVaultReference : functionStorageAccountConnectionString
        }
        {
          name: 'WEBSITE_CONTENTSHARE'
          value: toLower(functionAppName)
        }
        {
          name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
          value: appInsightsResource.properties.InstrumentationKey
        }
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          value: 'InstrumentationKey=${appInsightsResource.properties.InstrumentationKey}'
        }
        {
          name: 'FUNCTIONS_WORKER_RUNTIME'
          value: functionsWorkerRuntime
        }
        {
          name: 'FUNCTIONS_EXTENSION_VERSION'
          value: functionsExtensionVersion
        }
        {
          name: 'WEBSITE_NODE_DEFAULT_VERSION'
          value: nodeDefaultVersion
        }
        {
          name: 'USE32BITWORKERPROCESS'
          value: use32BitProcess
        }
        {
          name: 'NET_FRAMEWORK_VERSION'
          value: netFrameworkVersion
        }
        {
          name: 'WEBSITE_USE_PLACEHOLDER_DOTNETISOLATED'
          value: usePlaceholderDotNetIsolated
        }
      ]
    }
    scmSiteAlsoStopped: false
    clientAffinityEnabled: false
    clientCertEnabled: false
    hostNamesDisabled: false
    dailyMemoryTimeQuota: 0
    httpsOnly: true
    redundancyMode: 'None'
    publicNetworkAccess: publicNetworkAccess
    storageAccountRequired: false
    keyVaultReferenceIdentity: managedIdentityId // 'SystemAssigned'
  }
}

resource functionAppConfig 'Microsoft.Web/sites/config@2023-01-01' = {
  parent: functionAppResource
  name: 'web'
  properties: {
    numberOfWorkers: numberOfWorkers
    netFrameworkVersion: netFrameworkVersion
    linuxFxVersion: linuxFxVersion
    requestTracingEnabled: false
    remoteDebuggingEnabled: false
    httpLoggingEnabled: false
    acrUseManagedIdentityCreds: false
    logsDirectorySizeLimit: 35
    detailedErrorLoggingEnabled: false
    scmType: 'None'
    use32BitWorkerProcess: false
    webSocketsEnabled: false
    alwaysOn: false
    managedPipelineMode: 'Integrated'
    virtualApplications: [
      {
        virtualPath: '/'
        physicalPath: 'site\\wwwroot'
        preloadEnabled: false
      }
    ]
    loadBalancing: 'LeastRequests'
    experiments: {
      rampUpRules: []
    }
    autoHealEnabled: false
    vnetRouteAllEnabled: false
    vnetPrivatePortsCount: 0
    cors: {
      allowedOrigins: [
        'https://portal.azure.com'
      ]
      supportCredentials: false
    }
    localMySqlEnabled: false
    ipSecurityRestrictions: [
      {
        ipAddress: 'Any'
        action: 'Allow'
        priority: 2147483647
        name: 'Allow all'
        description: 'Allow all access'
      }
    ]
    scmIpSecurityRestrictions: [
      {
        ipAddress: 'Any'
        action: 'Allow'
        priority: 2147483647
        name: 'Allow all'
        description: 'Allow all access'
      }
    ]
    scmIpSecurityRestrictionsUseMain: false
    http20Enabled: false
    minTlsVersion: '1.2'
    scmMinTlsVersion: '1.2'
    ftpsState: 'FtpsOnly'
    preWarmedInstanceCount: 0
    functionAppScaleLimit: 200
    functionsRuntimeScaleMonitoringEnabled: false
    minimumElasticInstanceCount: 0
    azureStorageAccounts: { }
  }
}

resource functionAppBinding 'Microsoft.Web/sites/hostNameBindings@2018-11-01' = {
    name: '${functionAppResource.name}.azurewebsites.net'
    parent: functionAppResource
    properties: {
        siteName: functionAppName
        hostNameType: 'Verified'
    }
}

resource functionAppMetricLogging 'Microsoft.Insights/diagnosticSettings@2021-05-01-preview' = {
  name: '${functionAppResource.name}-metrics'
  scope: functionAppResource
  properties: {
    workspaceId: workspaceId
    metrics: [
      {
        category: 'AllMetrics'
        enabled: true
      }
    ]
  }
}
// https://learn.microsoft.com/en-us/azure/app-service/troubleshoot-diagnostic-logs
resource functionAppAuditLogging 'Microsoft.Insights/diagnosticSettings@2021-05-01-preview' = {
  name: '${functionAppResource.name}-logs'
  scope: functionAppResource
  properties: {
    workspaceId: workspaceId
    logs: [
      {
        category: 'FunctionAppLogs'
        enabled: true
      }
    ]
  }
}
resource appServiceMetricLogging 'Microsoft.Insights/diagnosticSettings@2021-05-01-preview' = {
  name: '${appServiceResource.name}-metrics'
  scope: appServiceResource
  properties: {
    workspaceId: workspaceId
    metrics: [
      {
        category: 'AllMetrics'
        enabled: true
      }
    ]
  }
}

// --------------------------------------------------------------------------------
output id string = functionAppResource.id
output hostname string = functionAppResource.properties.defaultHostName
output name string = functionAppName
output insightsName string = functionInsightsName
output insightsKey string = appInsightsResource.properties.InstrumentationKey
output storageAccountName string = functionStorageAccountName
