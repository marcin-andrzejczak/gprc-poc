using GrpcPoc.Server;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace GrpcPoc.Benchmark.Clients
{
    public class RestClient
    {
        private static readonly HttpClient client = new();

        public RestClient()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        
        //public async Task<ResourceTokenReply> GetTokenAsync(string databaseName, string collectionName)
        //{
        //    var url = string.Format("http://localhost:5100/api/cosmos/token?database={0}&collection={1}", databaseName, collectionName);
        //    var result = await client.GetFromJsonAsync<ResourceTokenReply>(url);
        //    return result!;
        //}

        public async Task<BenchmarkTextReply> GetBenchmarkTextReplyAsync(string text)
        {
            var url = string.Format("http://localhost:5100/api/cosmos/benchmark?text={0}", text);
            var result = await client.GetFromJsonAsync<BenchmarkTextReply>(url);
            return result!;
        }
    }
}
