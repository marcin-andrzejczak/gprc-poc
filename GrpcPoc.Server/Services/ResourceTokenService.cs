using Microsoft.Azure.Cosmos;

namespace GrpcPoc.Server.Services
{
    public class ResourceTokenService : IResourceTokenService
    {
        private readonly CosmosClient _client;

        public ResourceTokenService(CosmosClient client)
        {
            _client = client;
        }

        public async Task<string> GetEncodedResourceTokenAsync(
            string databaseName,
            string containerName,
            CancellationToken cancellationToken = default)
        {
            await _client.CreateDatabaseIfNotExistsAsync(databaseName, cancellationToken: cancellationToken);
            var database = _client.GetDatabase(databaseName);

            var createContainerOptions = new ContainerProperties { Id = containerName, PartitionKeyPath = "/partitionKey" };
            await database.CreateContainerIfNotExistsAsync(createContainerOptions, cancellationToken: cancellationToken);
            var container = database.GetContainer(containerName);

            await database.UpsertUserAsync("test", cancellationToken: cancellationToken);
            var user = database.GetUser("test");

            await user.UpsertPermissionAsync(
                new PermissionProperties(
                    id: "some-permission",
                    permissionMode: PermissionMode.All,
                    container: container
                ),
                cancellationToken: cancellationToken
            );
            var permission = await user.GetPermission("some-permission").ReadAsync(cancellationToken: cancellationToken);

            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(permission.Resource.Token));
        }
    }
}
