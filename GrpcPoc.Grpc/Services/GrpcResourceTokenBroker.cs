using Grpc.Core;
using static GrpcPoc.Grpc.GrpcResourceTokenBroker;

namespace GrpcPoc.Grpc.Services
{
    public class GrpcResourceTokenBroker : GrpcResourceTokenBrokerBase
    {
        public override Task<GrpcBenchmarkTextReply> GetBenchmarkTextBack(GrpcBenchmarkTextRequest request, ServerCallContext context)
        {
            return Task.FromResult(new GrpcBenchmarkTextReply
            {
                Text = request.Text
            });
        }
    }
}
