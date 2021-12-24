# git
LASTTAG := $(shell git tag --sort=committerdate | tail -1)

# docker option
DTAG   ?= $(LASTTAG)
DFNAME ?= Dockerfile
DRNAME ?= docker.io/aryazanov/pipeline-manager/devcontainer

# ------------------------------------------------------------------------------
#  container

.PHONY: container
container:
	@docker build -t $(DRNAME):$(DTAG) -f ./.devcontainer/$(DFNAME) .
	@docker push $(DRNAME):$(DTAG)