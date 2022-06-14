using GrpcPoc.Rest.Models;
using Microsoft.AspNetCore.Mvc;

namespace GrpcPoc.Hybrid.Controllers
{
    [ApiController]
    [Route("/api/cosmos")]
    public class ResourceTokenController : ControllerBase
    {

        [HttpGet("benchmark")]
        public Task<RestBenchmarkTextReply> GetBenchmarkTextReply([FromQuery] RestBenchmarkTextReply request)
        {
            return Task.FromResult(new RestBenchmarkTextReply
            {
                Text = request.Text
            });
        }
    }
}
