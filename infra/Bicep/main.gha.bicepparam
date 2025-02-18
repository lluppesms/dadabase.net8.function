using 'main.bicep'

param appName = '#{APP_NAME}#'
param environmentCode = '#{envCode}#'
param location = '#{RESOURCEGROUP_LOCATION}#'
param storageSku = '#{STORAGE_SKU}#'
param functionAppSku = '#{FUNCTION_APP_SKU}#'
param functionAppSkuFamily = '#{FUNCTION_APP_SKU_FAMILY}#'
param functionAppSkuTier = '#{FUNCTION_APP_SKU_TIER}#'
param adminUserId = '#{ADMIN_PRINCIPAL_ID}#'
param deDuplicateSecrets = #{DEDUP_SECRETS}#
