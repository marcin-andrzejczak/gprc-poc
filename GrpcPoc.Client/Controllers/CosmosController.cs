using Grpc.Core;
using GrpcPoc.Client.Models;
using GrpcPoc.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.Diagnostics;
using System.Text;
using static GrpcPoc.Server.ResourceTokenBroker;

namespace GrpcPoc.Client.Controllers
{
    [ApiController]
    [Route("cosmos")]
    public class CosmosController : ControllerBase
    {
        private const string DatabaseName = "PeopleDatabase";
        private const string CollectionName = "People";

        private readonly IConfiguration _configuration;
        private readonly ResourceTokenBrokerClient _brokerClient;
        private readonly IHttpClientFactory _httpFactory;

        public CosmosController(
            IConfiguration configuration,
            ResourceTokenBrokerClient brokerClient,
            IHttpClientFactory httpFactory)
        {
            _configuration = configuration;
            _brokerClient = brokerClient;
            _httpFactory = httpFactory;
        }

        [HttpPost("grpc/people")]
        public async Task<ActionResult<EntityResponse<DataEntity<Person>>>> AddGrpcEntity([FromBody] Person entity, CancellationToken cancellationToken)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var request = new ResourceTokenRequest
            {
                Database = DatabaseName,
                Collection = CollectionName
            };
            var tokenResponse = await _brokerClient.GetResourceTokenAsync(request, new CallOptions(cancellationToken: cancellationToken));
            var tokenFetchTime = stopwatch.ElapsedMilliseconds;

            stopwatch.Restart();
            var result = await AddEntityUsingTokenAsync(entity, tokenResponse.Token, "grpc", cancellationToken);
            var entityCreationTime = stopwatch.ElapsedMilliseconds;

            stopwatch.Stop();
            return Ok(new EntityResponse<DataEntity<Person>>(result.Resource, tokenFetchTime, entityCreationTime));
        }

        [HttpPost("rest/people")]
        public async Task<ActionResult<EntityResponse<DataEntity<Person>>>> AddRestEntity([FromBody] Person entity, CancellationToken cancellationToken)
        {
            var client = _httpFactory.CreateClient("ServerApi");
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var url = string.Format("api/cosmos/token?database={0}&collection={1}", DatabaseName, CollectionName);
            var tokenResponse = await client.GetFromJsonAsync<ResourceTokenReply>(url, cancellationToken);
            var tokenFetchTime = stopwatch.ElapsedMilliseconds;

            stopwatch.Restart();
            var result = await AddEntityUsingTokenAsync(entity, tokenResponse!.Token, "rest", cancellationToken);
            var entityCreationTime = stopwatch.ElapsedMilliseconds;

            stopwatch.Stop();
            return Ok(new EntityResponse<DataEntity<Person>>(result.Resource, tokenFetchTime, entityCreationTime));
        }

        private Task<ItemResponse<DataEntity<TEntity>>> AddEntityUsingTokenAsync<TEntity>(
            TEntity entity,
            string encodedToken,
            string partitionKey,
            CancellationToken cancellationToken = default)
        {
            var token = Encoding.UTF8.GetString(Convert.FromBase64String(encodedToken));
            var client = new CosmosClient(_configuration["Cosmos:AccountUrl"], token, new CosmosClientOptions()
            {
                SerializerOptions = new CosmosSerializationOptions()
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                }
            });

            var database = client.GetDatabase(DatabaseName);
            var container = database.GetContainer(CollectionName);
            var dataEntity = new DataEntity<TEntity>(entity, partitionKey);

            return container.UpsertItemAsync(dataEntity, new PartitionKey(dataEntity.PartitionKey), cancellationToken: cancellationToken);
        }
    }
}