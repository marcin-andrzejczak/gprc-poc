namespace GrpcPoc.Server.Services
{
    public interface IResourceTokenService
    {
        Task<string> GetEncodedResourceTokenAsync(string databaseName, string containerName, CancellationToken cancellationToken = default);
    }
}