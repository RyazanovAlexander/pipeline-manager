BUILDDIR ?= build

# git
LASTTAG := $(shell git tag --sort=committerdate | tail -1)

# docker option
DTAG   ?= $(LASTTAG)
DFNAME ?= Dockerfile
DRNAME ?= docker.io/aryazanov/pipeline-manager/worker/pipeline-agent

# ------------------------------------------------------------------------------
#  proto

.PHONY: proto
proto:
	@protoc -I ../Worker.Proto \
            --csharp_out=. \
            --grpc_out=. \
           ../Worker.Proto/exec.proto \
           --plugin=protoc-gen-grpc=../../Tools/grpc_csharp_plugin.exe

# ------------------------------------------------------------------------------
#  container

.PHONY: container
container:
	@docker build -t $(DRNAME):$(DTAG) -f ./$(BUILDDIR)/$(DFNAME) .
	@docker push $(DRNAME):$(DTAG)