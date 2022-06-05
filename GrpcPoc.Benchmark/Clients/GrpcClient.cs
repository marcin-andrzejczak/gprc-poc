using Grpc.Net.Client;
using GrpcPoc.Server;

namespace GrpcPoc.Benchmark.Clients
{
    public class GrpcClient
    {
        private readonly GrpcChannel channel;
        private readonly ResourceTokenBroker.ResourceTokenBrokerClient client;
        public GrpcClient()
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            channel = GrpcChannel.ForAddress("http://localhost:5200");
            client = new ResourceTokenBroker.ResourceTokenBrokerClient(channel);
        }

        //public async Task<ResourceTokenReply> GetTokenAsync(string databaseName, string collectionName)
        //    => await client.GetResourceTokenAsync(new ResourceTokenRequest
        //    {
        //        Database = databaseName,
        //        Collection = collectionName
        //    });

        public async Task<BenchmarkTextReply> GetBenchmarkTextReplyAsync(string text)
            => await client.GetBenchmarkTextBackAsync(new BenchmarkTextRequest
            {
                Text = text
            });
    }
}
