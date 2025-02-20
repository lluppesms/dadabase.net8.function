# Set up GitHub Secrets

The GitHub workflows in this project require several secrets set at the repository level.

---

## Azure Resource Creation Credentials

You need to set up the Azure Credentials secret in the GitHub Secrets at the Repository level before you do anything else.

See [https://docs.microsoft.com/en-us/azure/azure-resource-manager/templates/deploy-github-actions](https://docs.microsoft.com/en-us/azure/azure-resource-manager/templates/deploy-github-actions) for more info.

To create these secrets, customize and run this command::

``` bash
gh auth login

gh secret set AZURE_CLIENT_ID -b <GUID>
gh secret set AZURE_TENANT_ID -b <GUID>
gh secret set AZURE_SUBSCRIPTION_ID -b <yourAzureSubscriptionId>
gh secret set ADMIN_IP_ADDRESS -b <yourIp>
gh secret set ADMIN_PRINCIPAL_ID -b <yourUserGuid>
```

---

## Bicep Configuration Values

These variables and secrets are used by the Bicep templates to configure the resource names that are deployed.  Make sure the App_Name variable is unique to your deploy. It will be used as the basis for the website name and for all the other Azure resources, which must be globally unique.
To create these additional secrets and variables, customize and run this command:

Required Repository Variables:

``` bash
gh variable set RESOURCEGROUP_PREFIX -b rg_dbf_gh
gh variable set RESOURCEGROUP_LOCATION -b eastus2
gh variable set APP_NAME -b <yourInitials>-dbf-gh
gh variable set STORAGE_SKU -b Standard_LRS
gh variable set FUNCTION_APP_SKU -b Y1
gh variable set FUNCTION_APP_SKU_FAMILY -b Y
gh variable set FUNCTION_APP_SKU_TIER -b Dynamic
```

---

## References

[Deploying ARM Templates with GitHub Actions](https://docs.microsoft.com/en-us/azure/azure-resource-manager/templates/deploy-github-actions)

[GitHub Secrets CLI](https://cli.github.com/manual/gh_secret_set)
