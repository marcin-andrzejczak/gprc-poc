using Grpc.Core;
using GrpcPoc.Server.Services;
using static GrpcPoc.Server.ResourceTokenBroker;

namespace GrpcPoc.Server.GRPCServices
{
    public class ResourceTokenBroker : ResourceTokenBrokerBase
    {
        private readonly IResourceTokenService _tokenService;

        public ResourceTokenBroker(IResourceTokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public override async Task<ResourceTokenReply> GetResourceToken(ResourceTokenRequest request, ServerCallContext context)
        {
            var token = await _tokenService.GetEncodedResourceTokenAsync(request.Database, request.Collection, context.CancellationToken);
            return new ResourceTokenReply
            {
                Token = token
            };
        }

        public override Task<BenchmarkTextReply> GetBenchmarkTextBack(BenchmarkTextRequest request, ServerCallContext context)
        {
            return Task.FromResult(new BenchmarkTextReply
            {
                Text = request.Text
            });
        }
    }
}
