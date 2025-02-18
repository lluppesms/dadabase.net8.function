// ----------------------------------------------------------------------------------------------------
// Assign roles to the service principal or a given user
// ----------------------------------------------------------------------------------------------------
// NOTE: this requires elevated permissions in the resource group
// Contributor is not enough, you need Owner or User Access Administrator
// ----------------------------------------------------------------------------------------------------
// For a list of Role Id's see https://learn.microsoft.com/en-us/azure/role-based-access-control/built-in-roles
// ----------------------------------------------------------------------------------------------------

param registryName string = ''
param storageAccountName string = ''
param aiSearchName string = ''
param aiServicesName string = ''
param keyVaultName string = ''
param identityPrincipalId string
@allowed(['ServicePrincipal', 'User'])
param principalType string = 'ServicePrincipal'

// ----------------------------------------------------------------------------------------------------
var roleDefinitions = loadJsonContent('../data/roleDefinitions.json')
var addRegistryRoles = !empty(registryName)
var addStorageRoles = !empty(storageAccountName)
var addSearchRoles = !empty(aiSearchName)
var addCogServicesRoles = !empty(aiServicesName)
var addKeyVaultRoles = !empty(keyVaultName)

// ----------------------------------------------------------------------------------------------------
// Storage Roles
// ----------------------------------------------------------------------------------------------------
resource storageAccount 'Microsoft.Storage/storageAccounts@2022-05-01' existing = if (addStorageRoles) {
  name: storageAccountName
}
resource storage_Role_BlobContributor 'Microsoft.Authorization/roleAssignments@2022-04-01' = if (addStorageRoles) {
  name: guid(storageAccount.id, identityPrincipalId, roleDefinitions.storage.blobDataContributorRoleId)
  scope: storageAccount
  properties: {
    principalId: identityPrincipalId
    principalType: principalType
    roleDefinitionId: resourceId('Microsoft.Authorization/roleDefinitions', roleDefinitions.storage.blobDataContributorRoleId)
    description: 'Permission for ${principalType} ${identityPrincipalId} to write to the storage account ${storageAccountName} Blob'
  }
}
resource storage_Role_TableContributor 'Microsoft.Authorization/roleAssignments@2022-04-01' = if (addStorageRoles) {
  name: guid(storageAccount.id, identityPrincipalId, roleDefinitions.storage.tableContributorRoleId)
  scope: storageAccount
  properties: {
    principalId: identityPrincipalId
    principalType: principalType
    roleDefinitionId: resourceId('Microsoft.Authorization/roleDefinitions', roleDefinitions.storage.tableContributorRoleId)
    description: 'Permission for ${principalType} ${identityPrincipalId} to write to the storage account ${storageAccountName} Table'
  }
}
resource storage_Role_QueueContributor 'Microsoft.Authorization/roleAssignments@2022-04-01' = if (addStorageRoles) {
  name: guid(storageAccount.id, identityPrincipalId, roleDefinitions.storage.queueDataContributorRoleId)
  scope: storageAccount
  properties: {
    principalId: identityPrincipalId
    principalType: principalType
    roleDefinitionId: resourceId('Microsoft.Authorization/roleDefinitions', roleDefinitions.storage.queueDataContributorRoleId)
    description: 'Permission for ${principalType} ${identityPrincipalId} to write to the storage account ${storageAccountName} Queue'
  }
}
resource roleAssignmentBlobDataOwner 'Microsoft.Authorization/roleAssignments@2022-04-01' = if (addStorageRoles) {
  name: guid(storageAccountName, identityPrincipalId, 'blobDataOwner')
  scope: storageAccount
  properties: {
    principalId: identityPrincipalId
    principalType: principalType
    roleDefinitionId: resourceId(
      'Microsoft.Authorization/roleDefinitions',
      roleDefinitions.storage.blobDataOwnerRoleId
    )
    description: 'Permission for ${principalType} ${identityPrincipalId} to own blob data in the storage account ${storageAccountName} Blob'
  }
}
resource roleAssignmentStorageAccountContributor 'Microsoft.Authorization/roleAssignments@2022-04-01' = if (addStorageRoles) {
  name: guid(storageAccountName, identityPrincipalId, 'storageAccountContributor')
  scope: storageAccount
  properties: {
    principalId: identityPrincipalId
    principalType: principalType
    roleDefinitionId: resourceId('Microsoft.Authorization/roleDefinitions', roleDefinitions.storage.storageAccountContributorRoleId)
    description: 'Permission for ${principalType} ${identityPrincipalId} to write to the storage account ${storageAccountName} Blob'
  }
}

// ----------------------------------------------------------------------------------------------------
// ACR Roles
// ----------------------------------------------------------------------------------------------------
resource registry 'Microsoft.ContainerRegistry/registries@2023-11-01-preview' existing = if (addRegistryRoles) {
  name: registryName
}
resource registry_Role_AcrPull 'Microsoft.Authorization/roleAssignments@2022-04-01' = if (addRegistryRoles) {
  name: guid(registry.id, identityPrincipalId, roleDefinitions.containerregistry.acrPullRoleId)
  scope: registry
  properties: {
    principalId: identityPrincipalId
    principalType: principalType
    roleDefinitionId: resourceId('Microsoft.Authorization/roleDefinitions', roleDefinitions.containerregistry.acrPullRoleId)
    description: 'Permission for ${principalType} ${identityPrincipalId} to pull images from the registry ${registryName}'
  }
}

// ----------------------------------------------------------------------------------------------------
// Key Vault Roles
// ----------------------------------------------------------------------------------------------------
resource keyVault 'Microsoft.KeyVault/vaults@2023-07-01' existing = {
  name: keyVaultName
}
resource keyVault_Role_SecretsUser 'Microsoft.Authorization/roleAssignments@2022-04-01' = if (addKeyVaultRoles) {
  name: guid(keyVault.id, identityPrincipalId, roleDefinitions.keyvault.secretsUser)
  scope: keyVault
  properties: {
    principalId: identityPrincipalId
    principalType: principalType
    roleDefinitionId: resourceId('Microsoft.Authorization/roleDefinitions', roleDefinitions.keyvault.secretsUser)
    description: 'Permission for ${principalType} ${identityPrincipalId} to user secrets in ${keyVaultName}'
  }
}
resource keyVault_Role_SecretsOfficer 'Microsoft.Authorization/roleAssignments@2022-04-01' = if (addKeyVaultRoles) {
  name: guid(keyVault.id, identityPrincipalId, roleDefinitions.keyvault.secretsOfficer)
  scope: keyVault
  properties: {
    principalId: identityPrincipalId
    principalType: principalType
    roleDefinitionId: resourceId('Microsoft.Authorization/roleDefinitions', roleDefinitions.keyvault.secretsOfficer)
    description: 'Permission for ${principalType} ${identityPrincipalId} to administer secrets in ${keyVaultName}'
  }
}

// ----------------------------------------------------------------------------------------------------
// Cognitive Services Roles
// ----------------------------------------------------------------------------------------------------
resource aiService 'Microsoft.CognitiveServices/accounts@2024-06-01-preview' existing = if (addCogServicesRoles) {
  name: aiServicesName
}
resource cognitiveServices_Role_OpenAIUser 'Microsoft.Authorization/roleAssignments@2022-04-01' = if (addCogServicesRoles) {
  name: guid(aiService.id, identityPrincipalId, roleDefinitions.openai.cognitiveServicesOpenAIUserRoleId)
  scope: aiService
  properties: {
    principalId: identityPrincipalId
    principalType: principalType
    roleDefinitionId: resourceId('Microsoft.Authorization/roleDefinitions', roleDefinitions.openai.cognitiveServicesOpenAIUserRoleId)
    description: 'Permission for ${principalType} ${identityPrincipalId} to be OpenAI User'
  }
}
resource cognitiveServices_Role_OpenAIContributor 'Microsoft.Authorization/roleAssignments@2022-04-01' = if (addCogServicesRoles) {
  name: guid(aiService.id, identityPrincipalId, roleDefinitions.openai.cognitiveServicesOpenAIContributorRoleId)
  scope: aiService
  properties: {
    principalId: identityPrincipalId
    principalType: principalType
    roleDefinitionId: resourceId('Microsoft.Authorization/roleDefinitions', roleDefinitions.openai.cognitiveServicesOpenAIContributorRoleId)
    description: 'Permission for ${principalType} ${identityPrincipalId} to be OpenAI Contributor'
  }
}
resource cognitiveServices_Role_User 'Microsoft.Authorization/roleAssignments@2022-04-01' = if (addCogServicesRoles) {
  name: guid(aiService.id, identityPrincipalId, roleDefinitions.openai.cognitiveServicesUserRoleId)
  scope: aiService
  properties: {
    principalId: identityPrincipalId
    principalType: principalType
    roleDefinitionId: resourceId('Microsoft.Authorization/roleDefinitions', roleDefinitions.openai.cognitiveServicesUserRoleId)
    description: 'Permission for ${principalType} ${identityPrincipalId} to be a Cognitive Services User'
  }
}
resource cognitiveServices_Role_Contributor 'Microsoft.Authorization/roleAssignments@2022-04-01' = if (addCogServicesRoles) {
  name: guid(aiService.id, identityPrincipalId, roleDefinitions.openai.cognitiveServicesContributorRoleId)
  scope: aiService
  properties: {
    principalId: identityPrincipalId
    principalType: principalType
    roleDefinitionId: resourceId('Microsoft.Authorization/roleDefinitions', roleDefinitions.openai.cognitiveServicesContributorRoleId)
    description: 'Permission for ${principalType} ${identityPrincipalId} to be a Cognitive Services Contributor'
  }
}

// ----------------------------------------------------------------------------------------------------
// Search Roles
// ----------------------------------------------------------------------------------------------------
resource searchService 'Microsoft.Search/searchServices@2024-06-01-preview' existing = if (addSearchRoles) {
  name: aiSearchName
}
resource search_Role_IndexDataContributor 'Microsoft.Authorization/roleAssignments@2022-04-01' = if (addSearchRoles) {
  name: guid(searchService.id, identityPrincipalId, roleDefinitions.search.indexDataContributorRoleId)
  scope: searchService
  properties: {
    principalId: identityPrincipalId
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', roleDefinitions.search.indexDataContributorRoleId)
    principalType: principalType
    description: 'Permission for ${principalType} ${identityPrincipalId} to use the modify search service indexes'
  }
}
resource search_Role_IndexDataReader 'Microsoft.Authorization/roleAssignments@2022-04-01' = if (addSearchRoles) {
  name: guid(searchService.id, identityPrincipalId, roleDefinitions.search.indexDataReaderRoleId)
  scope: searchService
  properties: {
    principalId: identityPrincipalId
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', roleDefinitions.search.indexDataReaderRoleId)
    principalType: principalType
    description: 'Permission for ${principalType} ${identityPrincipalId} to use the read search service indexes'
  }
}
resource search_Role_ServiceContributor 'Microsoft.Authorization/roleAssignments@2022-04-01' = if (addSearchRoles) {
  name: guid(searchService.id, identityPrincipalId, roleDefinitions.search.serviceContributorRoleId)
  scope: searchService
  properties: {
    principalId: identityPrincipalId
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', roleDefinitions.search.serviceContributorRoleId)
    principalType: principalType
    description: 'Permission for ${principalType} ${identityPrincipalId} to be a search service contributor'
  }
}
