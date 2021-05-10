# Command executor

Designed to start processes inside a container. Worker.CommandExecutor at startup connects to Worker.PipelineAgent via gRPC and waits for commands to be executed.