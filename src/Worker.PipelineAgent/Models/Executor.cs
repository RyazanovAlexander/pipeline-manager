using Exec;
using Grpc.Core;
using System;
using static Exec.ExecService;

namespace Worker.PipelineAgent.Models
{
    internal sealed class Executor: IDisposable
    {
        private readonly Channel _channel;
        private readonly ExecServiceClient _client;

        public string Name { get; }

        public Executor(string name, string target)
        {
            Name = name;

            _channel = new Channel(target, ChannelCredentials.Insecure);
            _client = new ExecServiceClient(_channel);
        }

        public ExecResult ExecuteCommand(string command)
        {
            return _client.ExecuteCommand(new ExecCommand { Command = command });
        }

        public void Dispose()
        {
            _channel.ShutdownAsync().Wait();
        }
    }
}
