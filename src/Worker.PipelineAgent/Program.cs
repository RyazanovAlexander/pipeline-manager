namespace Worker.PipelineAgent
{
    using System;
    using Grpc.Core;
    using Exec;

    internal class Program
    {
        private static void Main()
        {
            var channel = new Channel("127.0.0.1:50051", ChannelCredentials.Insecure);

            var client = new ExecService.ExecServiceClient(channel);

            var reply = client.ExecuteCommand(new ExecCommand
            {
                Command = "echo 1234 > 1.txt | cat 1.txt"
            });

            Console.WriteLine("Got: " + reply.Result);

            channel.ShutdownAsync().Wait();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}