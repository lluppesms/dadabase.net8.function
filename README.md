# .NET 8 Function Dad Joke API Example

## Introduction

This repository is an example of deploying a .NET 8 Function app into a Linux environment on Azure. The Function app is a simple API that returns a random dad joke with a few options like category list and search.

The project has  fully automated CI/CD pipelines with Bicep templates to deploy the application and infrastructure.

It can also be deployed via GitHub Actions and Azure DevOps CLI (azd), although those have not been tested and verified for .NET 8 yet.

[![Open in vscode.dev](https://img.shields.io/badge/Open%20in-vscode.dev-blue)][1]

[1]: https://vscode.dev/github/lluppesms/dadabase.net8.function/

[![azd Compatible](/Docs/images/AZD_Compatible.png)](/.azure/readme.md)

[![deploy.infra.and.website](https://github.com/lluppesms/dadabase.net8.function/actions/workflows/deploy-infra-website.yml/badge.svg)](https://github.com/lluppesms/dadabase.net8.function/actions/workflows/deploy-infra-website.yml)

---

## Deployment Options

1. [Deploy using Azure DevOps](./.azdo/pipelines/readme.md)

1. *[Deploy using AZD Command Line Tool](./Docs/AzdDeploy.md)
(untested for .NET8...  coming soon!)*

1. *[Deploy using GitHub Actions](./.github/workflows-readme.md) (untested for .NET8...  coming soon!)*

<!-- [A good example of a DadJoke API](https://icanhazdadjoke.com/api) -->
