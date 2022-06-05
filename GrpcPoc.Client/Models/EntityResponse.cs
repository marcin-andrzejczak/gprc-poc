namespace GrpcPoc.Client.Models
{
    public class EntityResponse<TEntity>
    {
        public TEntity Entity { get; set;}
        public long TokenFetchTime { get; set; }
        public long EntityCreationTime { get; set;}

        public EntityResponse(TEntity entity, long tokenFetchTime, long entityCreationTime)
        {
            Entity = entity;
            TokenFetchTime = tokenFetchTime;
            EntityCreationTime = entityCreationTime;
        }
    }
}
