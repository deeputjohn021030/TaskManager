namespace TaskManager.API.Models
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CollectionName { get; set; } = null!;         // For Tasks
        public string UserCollectionName { get; set; } = null!;     // For Users

    }
}
