# Worker.PipelineAgent

Worker.PipelineAgent is located in each Pod in a separate container. Performs the following functions:
1) Picks up tasks from the Orleans cluster for execution.
2) Sequentially executes commands from the task on containers within the boundaries of its Pod. To do this, all Worker.CommandExecutor's connect to Worker.PipelineAgent via gRPC.