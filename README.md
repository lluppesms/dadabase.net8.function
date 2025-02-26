# .NET 8 Function Dad Joke API Example

Where does a geeky Dad store all of his Dad jokes? In a dad-a-base, of course!

## Introduction

This repository is an example of deploying a .NET 8 app into an Azure Function. The Function app itself is a very simple API that just reads a JSON file and returns a random dad joke, along with a few other APIs like search and category list.

---

This project is intended as a good example of using Infrastructure as Code (IaC) to deploy and manage the Azure resources, utilizing [Bicep](https://learn.microsoft.com/en-us/azure/azure-resource-manager/bicep/overview) to deploy Azure resources declaratively.

The project also has fully automated CI/CD pipelines to deploy both the infrastructure and the application, so you can literally run one pipeline and have it create the Azure Resources, build the program, unit test the program, deploy the program to Azure, and run [Playwright](https://playwright.dev/dotnet/) smoke tests after it is deployed.

---

Deployment Options include:

* [Deploy using Azure DevOps Pipelines](./.azdo/pipelines/readme.md)
* [Deploy using GitHub Actions](./.github/workflows-readme.md)
* [Deploy using AZD Command Line Tool](./Docs/AzdDeploy.md)

---

[![Open in vscode.dev](https://img.shields.io/badge/Open%20in-vscode.dev-blue)][1]

[1]: https://vscode.dev/github/lluppesms/dadabase.net8.function/

[![azd Compatible](/Docs/images/AZD_Compatible.png)](/.azure/readme.md)

[![deploy.infra.and.website](https://github.com/lluppesms/dadabase.net8.function/actions/workflows/4-bicep-build-deploy-app.yml/badge.svg)](https://github.com/lluppesms/dadabase.net8.function/actions/workflows/4-bicep-build-deploy-app.yml)

---

License: [MIT](./LICENSE)

<!-- [A good example of a DadJoke API](https://icanhazdadjoke.com/api) -->
