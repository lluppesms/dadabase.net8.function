# Set up GitHub Actions (beta documentation!)

The GitHub workflows in this project require several secrets set at the repository level or at the environment level.

These docs still need some work!
---

## Workflow Definitions

- **[1-bicep-only.yml](./workflows/1-bicep-only.yml):** Deploys the main.bicep template with all new resources and does nothing else
- **[3-build-deploy-app.yml](./workflows/3-build-deploy-app.yml):** Builds the app and deploys it to Azure - this should happen automatically after each check-in to main
- **[4-bicep-build-deploy-app.yml](./workflows/4-bicep-build-deploy-app.yml):** Builds the app and deploys it to Azure - this should happen automatically after each check-in to main
- **[7-scan-devsecops.yml](./workflows/7-scan-devsecops.yml):** Runs a security scan on the application and infrastructure code.
- **[8-scan-codeql.yml](./workflows/8-scan-codeql.yml):** Runs a scheduled CodeQL scan of the app for application review

---

## Azure Credentials

You will need to set up the Azure Credentials secrets in the GitHub Secrets at the Repository level (or the environment level) before you do anything else.

See [https://docs.microsoft.com/en-us/azure/azure-resource-manager/templates/deploy-github-actions](https://docs.microsoft.com/en-us/azure/azure-resource-manager/templates/deploy-github-actions) for more info on how to create the service principal and set up these credentials.

> Note: this service principal must have contributor rights to your subscription (or resource group) to deploy the resources.

You can customize and run the following commands, or you can set these secrets up manually by going to the Settings -> Secrets -> Actions -> Secrets.

```bash
gh secret set --env <ENV-NAME> AZURE_SUBSCRIPTION_ID -b <yourAzureSubscriptionId>
gh secret set --env <ENV-NAME> AZURE_TENANT_ID -b <GUID-Entra-tenant-where-SP-lives>
gh secret set --env <ENV-NAME> CICD_CLIENT_ID -b <GUID-application/client-Id>
```

These two secrets are optional if you want to grant an administrator access to the Key Vault and ACR.  

```bash
gh secret set ADMIN_IP_ADDRESS 192.168.1.1
gh secret set ADMIN_PRINCIPAL_ID <yourGuid>
```

---

## Bicep Configuration Values (TBD!)

These values are used by the Bicep templates to configure the resource names that are deployed. Make sure the App_Name variable is unique to your deploy. It will be used as the basis for the application name and for all the other Azure resources, some of which must be globally unique.

> If you desire different names or values for your DEV/QA/PROD environments, you can set up the variables at the Environment level instead of the Repository level.

You can customize and run the following commands (or just set it up manually by going to the Settings -> Secrets -> Actions -> Variables).  Replace '<<YOURAPPNAME>>' with a value that is unique to your deployment, which can contain dashes or underscores (i.e. 'xxx-doc-review'). APP_NAME_NO_DASHES should be the same but without dashes.

These should be set at the repository level and may be the same for all environments, although you could set them up at the environment level if you want them to unique.

```bash
gh variable set APP_NAME -b <<YOUR-APP-NAME>>
gh variable set RESOURCEGROUP_LOCATION -b eastus2
```

---

## References

- [Deploying ARM Templates with GitHub Actions](https://docs.microsoft.com/en-us/azure/azure-resource-manager/templates/deploy-github-actions)
- [GitHub Secrets CLI](https://cli.github.com/manual/gh_secret_set)

---

[Home Page](../README.md)
