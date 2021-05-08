BUILDDIR ?= build

# git
LASTTAG := $(shell git tag --sort=committerdate | tail -1)

# docker option
DTAG   ?= $(LASTTAG)
DFNAME ?= Dockerfile
DRNAME ?= docker.io/aryazanov/pipeline-agent

# ------------------------------------------------------------------------------
#  proto

.PHONY: proto
proto:
	@protoc -I ../Worker.Proto \
            --csharp_out=./Proto \
            --grpc_out=./Proto \
            ../Worker.Proto/exec.proto \
            --plugin=protoc-gen-grpc=../../Tools/grpc_csharp_plugin.exe

# ------------------------------------------------------------------------------
#  container

.PHONY: container
container:
	@docker build -t $(DRNAME):$(DTAG) -f ./$(DFNAME) ../../.
	@docker push $(DRNAME):$(DTAG)

# ------------------------------------------------------------------------------
#  example-echo

.PHONY: example-echo
example-echo:
	@skaffold dev -f ./Example/echo/skaffold.yaml --port-forward --no-prune=false --cache-artifacts=false

.PHONY: example-echo-delete
example-echo-delete:
	@skaffold delete -f ./Example/echo/skaffold.yaml