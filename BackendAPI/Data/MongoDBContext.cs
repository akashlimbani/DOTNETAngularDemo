using BackendAPI.Models.Common;
using BackendAPI.Models.Entity;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BackendAPI.Data
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(IOptions<MongoDBSettings> settings, IMongoClient client)
        {
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    }
}
