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
	@docker build -t $(DRNAME):$(DTAG) -f ./Dockerfile ../../.
	@docker push $(DRNAME):$(DTAG)

# ------------------------------------------------------------------------------
#  example-echo

.PHONY: example-echo
example-echo:
	@docker build -t aryazanov/example-echo:0.0.1 -f ./Example/echo/Dockerfile ./Example/echo/
	@skaffold dev -f ./Example/echo/skaffold.yaml

.PHONY: example-echo-delete
example-echo-delete:
	@skaffold delete -f ./Example/echo/skaffold.yaml