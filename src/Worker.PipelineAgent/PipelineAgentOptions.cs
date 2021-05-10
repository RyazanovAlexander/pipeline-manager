namespace Worker.PipelineAgent
{
    internal sealed class PipelineAgentOptions
    {
        public const string SectionName = "PIPELINE_AGENT";

        public string Name { get; set; }

        public ExecutorOption[] Executors { get; set; }
    }

    internal sealed class ExecutorOption
    {
        public string Name { get; set; }

        public string Target { get; set; }
    }
}
