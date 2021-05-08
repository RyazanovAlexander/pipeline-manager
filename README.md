# Pipeline manager
Cloud-native pipeline orchestration platform.

## Project structure
The project consists of several repositories:
1) [pipeline-manager](https://pingcap.com) - cloud-native, distributed SQL database for elastic scale and real-time analytics.
Used to store [App Grains](https://dotnet.github.io/orleans/docs/index.html) states and task list.
2) [Prometheus Operator](https://prometheus-operator.dev/) - provides Kubernetes native deployment and management of Prometheus and related monitoring components. The purpose of this project is to simplify and automate the configuration of a Prometheus based monitoring stack for Kubernetes clusters.
Used for storing and analyzing metrics and logs.
3) [KEDA](https://keda.sh/) - allows for fine-grained autoscaling (including to/from zero) for event driven Kubernetes workloads. KEDA serves as a Kubernetes Metrics Server and allows users to define autoscaling rules using a dedicated Kubernetes custom resource definition.
Used for scaling platform services and private resources of user applications.

## Install


## Uninstall
