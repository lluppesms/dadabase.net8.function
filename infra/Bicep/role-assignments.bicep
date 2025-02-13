// Assign roles to the service principal 
// NOTE: this requires elevated permissions in the resource group
// Contributor is not enough, you need Owner or User Access Administrator
// See https://learn.microsoft.com/en-us/azure/role-based-access-control/built-in-roles

param identityPrincipalId string
@allowed(['ServicePrincipal', 'User'])
param principalType string = 'ServicePrincipal'

param storageAccountName string
//param registryName string

var roleDefinitions = loadJsonContent('./data/roleDefinitions.json')

resource storageResource 'Microsoft.Storage/storageAccounts@2022-05-01' existing = {
  name: storageAccountName
}

resource roleAssignmentBlobDataOwner 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(storageAccountName, identityPrincipalId, 'blobDataOwner')
  scope: storageResource
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
resource roleAssignmentStorageAccountContributor 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(storageAccountName, identityPrincipalId, 'storageAccountContributor')
  scope: storageResource
  properties: {
    principalId: identityPrincipalId
    principalType: principalType
    roleDefinitionId: resourceId('Microsoft.Authorization/roleDefinitions', roleDefinitions.storage.storageAccountContributorRoleId)
    description: 'Permission for ${principalType} ${identityPrincipalId} to write to the storage account ${storageAccountName} Blob'
  }
}
resource roleAssignmentBlobContributor 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(storageAccountName, identityPrincipalId, 'blobContributor')
  scope: storageResource
  properties: {
    principalId: identityPrincipalId
    principalType: principalType
    roleDefinitionId: resourceId('Microsoft.Authorization/roleDefinitions', roleDefinitions.storage.blobDataContributorRoleId)
    description: 'Permission for ${principalType} ${identityPrincipalId} to write to the storage account ${storageAccountName} Blob'
  }
}

resource roleAssignmentTableContributor 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(storageAccountName, identityPrincipalId, 'tableContributor')
  scope: storageResource
  properties: {
    principalId: identityPrincipalId
    principalType: principalType
    roleDefinitionId: resourceId('Microsoft.Authorization/roleDefinitions', roleDefinitions.storage.tableContributorRoleId)
    description: 'Permission for ${principalType} ${identityPrincipalId} to write to the storage account ${storageAccountName} Table'
  }
}

resource roleAssignmentQueueContributor 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(storageAccountName, identityPrincipalId, 'queueContributor')
  scope: storageResource
  properties: {
    principalId: identityPrincipalId
    principalType: principalType
    roleDefinitionId: resourceId('Microsoft.Authorization/roleDefinitions', roleDefinitions.storage.queueDataContributorRoleId)
    description: 'Permission for ${principalType} ${identityPrincipalId} to write to the storage account ${storageAccountName} Queue'
  }
}

// resource registry 'Microsoft.ContainerRegistry/registries@2023-11-01-preview' existing = {
//   name: registryName
// }

// resource roleAssignmentAcrPull 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
//   name: guid(registryName, identityPrincipalId, 'acrPull')
//   scope: registry
//   properties: {
//     principalId: identityPrincipalId
//     principalType: principalType
//     roleDefinitionId: resourceId('Microsoft.Authorization/roleDefinitions', roleDefinitions.containerregistry.acrPullRoleId)
//     description: 'Permission for ${principalType} ${identityPrincipalId} to pull images from the registry ${registryName}'
//   }
// }


// // See https://docs.microsoft.com/azure/role-based-access-control/role-assignments-template#new-service-principal
// resource roleAssignmentCognitiveServicesOpenAIContributor 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
//   name: guid(resourceGroup().id, identityPrincipalId, roleDefinitions.openai.cognitiveServicesOpenAIContributorRoleId)
//   // TODO: this is assigned on RG level, but should be on the cognitive service level
//   properties: {
//     principalId: identityPrincipalId
//     principalType: principalType
//     roleDefinitionId: resourceId('Microsoft.Authorization/roleDefinitions', roleDefinitions.openai.cognitiveServicesOpenAIContributorRoleId)
//     description: 'Permission for ${principalType} ${identityPrincipalId} to use the OpenAI cognitive services'
//   }
// }

// resource roleAssignmentCognitiveServicesUserRole 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
//   name: guid(resourceGroup().id, identityPrincipalId, roleDefinitions.openai.cognitiveServicesUser)
//   // TODO: this is assigned on RG level, but should be on the cognitive service level
//   properties: {
//     principalId: identityPrincipalId
//     principalType: principalType
//     roleDefinitionId: resourceId('Microsoft.Authorization/roleDefinitions', roleDefinitions.openai.cognitiveServicesUser)
//     description: 'Permission for ${principalType} ${identityPrincipalId} to use the Cognitive Services'
//   }
// }

// // See https://docs.microsoft.com/azure/role-based-access-control/role-assignments-template#new-service-principal
// resource roleAssignmentSearchIndexDataContributor 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
//   name: guid(resourceGroup().id, identityPrincipalId, roleDefinitions.search.indexDataContributorRoleId)
//   // TODO: this is assigned on RG level, but should be on the cognitive service level
//   properties: {
//     principalId: identityPrincipalId
//     roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', roleDefinitions.search.indexDataContributorRoleId)
//     principalType: principalType
//     description: 'Permission for ${principalType} ${identityPrincipalId} to use the modify search service indexes'
//   }
// }

// resource roleAssignmentSearchServiceContributor 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
//   name: guid(resourceGroup().id, identityPrincipalId, roleDefinitions.search.serviceContributorRoleId)
//   // TODO: this is assigned on RG level, but should be on the cognitive service level
//   properties: {
//     principalId: identityPrincipalId
//     roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', roleDefinitions.search.serviceContributorRoleId)
//     principalType: principalType
//     description: 'Permission for ${principalType} ${identityPrincipalId} to use the search service'
//   }
// }
