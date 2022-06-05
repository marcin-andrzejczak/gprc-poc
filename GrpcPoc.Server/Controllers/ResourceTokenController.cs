using GrpcPoc.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace GrpcPoc.Server.Controllers
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
        public async Task<ResourceTokenReply> GetResourceToken([FromQuery] ResourceTokenRequest request, CancellationToken cancellationToken)
        {
            var token = await _tokenService.GetEncodedResourceTokenAsync(request.Database, request.Collection, cancellationToken);
            return new ResourceTokenReply
            {
                Token = token
            };
        }

        [HttpGet("benchmark")]
        public Task<BenchmarkTextReply> GetBenchmarkTextReply([FromQuery] BenchmarkTextReply request)
        {
            return Task.FromResult(new BenchmarkTextReply
            {
                Text = request.Text
            });
        }
    }
}
