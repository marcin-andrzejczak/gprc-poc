using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using GrpcPoc.Benchmark.Clients;

namespace GrpcPoc.Benchmark
{
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser]
    public class ApiBenchmark
    {
        [Params(100, 200, 500, 1000)]
        public int IterationCount;
        private readonly RestClient _restClient = new RestClient();
        private readonly GrpcClient _grpcClient = new GrpcClient();

        private const string BenchmarkText = "qwerty";

        [Benchmark]
        public async Task GetRestBenchmarkTestAsync()
        {
            for (int i = 0; i < IterationCount; i++)
            {
                await _restClient.GetBenchmarkTextReplyAsync(BenchmarkText);
            }
        }
        [Benchmark]
        public async Task GetGrpcBenchmarkTestAsync()
        {
            for (int i = 0; i < IterationCount; i++)
            {
                await _grpcClient.GetBenchmarkTextReplyAsync(BenchmarkText);
            }
        }
    }
}
