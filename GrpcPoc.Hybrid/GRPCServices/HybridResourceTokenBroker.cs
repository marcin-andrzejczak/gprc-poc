using Grpc.Core;
using GrpcPoc.Hybrid.Services;
using static GrpcPoc.Hybrid.HybridResourceTokenBroker;

namespace GrpcPoc.Hybrid.GRPCServices
{
    public class HybridResourceTokenBroker : HybridResourceTokenBrokerBase
    {
        private readonly IResourceTokenService _tokenService;

        public HybridResourceTokenBroker(IResourceTokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public override async Task<HybridResourceTokenReply> GetResourceToken(HybridResourceTokenRequest request, ServerCallContext context)
        {
            var token = await _tokenService.GetEncodedResourceTokenAsync(request.Database, request.Collection, context.CancellationToken);
            return new HybridResourceTokenReply
            {
                Token = token
            };
        }

        public override Task<HybridBenchmarkTextReply> GetBenchmarkTextBack(HybridBenchmarkTextRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HybridBenchmarkTextReply
            {
                Text = request.Text
            });
        }
    }
}
