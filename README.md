# Pipeline manager
Cloud-native pipeline orchestration platform.

## Motivation


## Usage scenarios


## Project structure
The project consists of several repositories:
1) pipeline-manager - contains documentation, CI/CD and links to other repositories.
2) [pipeline-manager.infrastructure](https://github.com/RyazanovAlexander/pipeline-manager.infrastructure) - contains the IaaC used by the platform.
3) [pipeline-manager.platform](https://github.com/RyazanovAlexander/pipeline-manager.platform) - contains the main platform services: ApiGateway, Pipeline manager cluster based on [Virtual Actor Model](https://dotnet.github.io/orleans/) and client to cluster for workers.
4) [pipeline-manager.platform.app-deployer](https://github.com/RyazanovAlexander/pipeline-manager.platform.app-deployer) - a tool for deploying applications that extend the functionality of the platform.
5) [pipeline-manager.worker.command-executor](https://github.com/RyazanovAlexander/pipeline-manager.worker.command-executor) - utility used by Pipeline workers to execute processes in pod containers.
6) [pipeline-manager.applications](https://github.com/RyazanovAlexander/pipeline-manager.applications) - directory with applications installed using [AppDeployer](https://github.com/RyazanovAlexander/pipeline-manager.platform.app-deployer).

## Local development requirements
Tools:
1) [Helm](https://helm.sh) v3.5.3+
2) [Skaffold](https://skaffold.dev) v1.21.0+
3) [Minikube](https://minikube.sigs.k8s.io) v1.18.1+
4) [Docker](https://www.docker.com) v20.10.5+
5) [kubectl](https://kubernetes.io/docs/tasks/tools) v1.20.5+
6) [Make](https://www.gnu.org/software/make/manual/make.html) v4.3+

Programming languages:
1) [Golang](https://golang.org/) v1.16.2+
2) [C#](https://dotnet.microsoft.com/download/dotnet/5.0) v9.0+, .NET 5.0+

IDE:
1) [Visual Studio Code](https://code.visualstudio.com)
2) [Visual Studio Community 2019](https://visualstudio.microsoft.com/ru/vs/community/)

## Installation


## Uninstall
