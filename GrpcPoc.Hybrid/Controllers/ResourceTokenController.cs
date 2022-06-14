using GrpcPoc.Hybrid.Services;
using Microsoft.AspNetCore.Mvc;

namespace GrpcPoc.Hybrid.Controllers
{
    [ApiController]
    [Route("/api/cosmos")]
    public class ResourceTokenController : ControllerBase
    {
        private readonly IResourceTokenService _tokenService;

        public ResourceTokenController(IResourceTokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet("token")]
        public async Task<HybridResourceTokenReply> GetResourceToken([FromQuery] HybridResourceTokenRequest request, CancellationToken cancellationToken)
        {
            var token = await _tokenService.GetEncodedResourceTokenAsync(request.Database, request.Collection, cancellationToken);
            return new HybridResourceTokenReply
            {
                Token = token
            };
        }

        [HttpGet("benchmark")]
        public Task<HybridBenchmarkTextReply> GetBenchmarkTextReply([FromQuery] HybridBenchmarkTextRequest request)
        {
            return Task.FromResult(new HybridBenchmarkTextReply
            {
                Text = request.Text
            });
        }
    }
}
