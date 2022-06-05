namespace GrpcPoc.Client.Models
{
    public class DataEntity<TEntity>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public TEntity Entity { get; set; }
        public string PartitionKey { get; set; }

        public DataEntity(TEntity entity, string partitionKey)
        {
            Entity = entity;
            PartitionKey = partitionKey;
        }
    }
}
