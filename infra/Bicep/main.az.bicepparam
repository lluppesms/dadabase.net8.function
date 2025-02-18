using 'main.bicep'

param appName = '#{appName}#'
param environmentCode = '#{environmentNameLower}#'
param location = '#{location}#'
param storageSku = '#{storageSku}#'
param functionAppSku = '#{functionAppSku}#'
param functionAppSkuFamily = '#{functionAppSkuFamily}#'
param functionAppSkuTier = '#{functionAppSkuTier}#'
param adminUserId = '#{adminUserId}#'
param deDuplicateSecrets = #{deDuplicateSecrets}#
