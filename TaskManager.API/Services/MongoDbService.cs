using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using TaskManager.API.Models;

namespace TaskManager.API.Services
{
    public class MongoDbService
    {
        private readonly IMongoCollection<TaskItem> _tasks;

        public MongoDbService(IOptions<MongoDbSettings> settings)
        {

            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _tasks = database.GetCollection<TaskItem>(settings.Value.CollectionName);
        }

        public async Task<List<TaskItem>> GetAsync() =>
            await _tasks.Find(_ => true).ToListAsync();

        public async Task<TaskItem?> GetAsync(string id)
        {
            if (!ObjectId.TryParse(id, out _))
                return null;

            return await _tasks.Find(t => t.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(TaskItem task)
        {
            if (string.IsNullOrEmpty(task.Id))
            {
                task.Id = ObjectId.GenerateNewId().ToString();
            }

            Console.WriteLine($"Inserting task: {task.Title} into collection {nameof(_tasks)}");
            await _tasks.InsertOneAsync(task);
        }
        public async Task UpdateAsync(string id, TaskItem updatedTask) =>
            await _tasks.ReplaceOneAsync(t => t.Id == id, updatedTask);

        public async Task RemoveAsync(string id) =>
            await _tasks.DeleteOneAsync(t => t.Id == id);

        public async Task<List<TaskItem>> GetTasksByUserIdAsync(string userId)
        {
            return await _tasks.Find(t => t.UserId == userId).ToListAsync();
        }
    }
}
