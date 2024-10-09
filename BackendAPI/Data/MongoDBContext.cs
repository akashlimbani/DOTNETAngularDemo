using BackendAPI.Models.Common;
using BackendAPI.Models.Entity;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BackendAPI.Data;

public class MongoDBContext(IOptions<MongoDBSettings> settings, IMongoClient client)
{
    private readonly IMongoDatabase _database = client.GetDatabase(settings.Value.DatabaseName);

    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");

    public IMongoCollection<BsonDocument> Counters => _database.GetCollection<BsonDocument>("counters");

    // Method to get the next sequence value for a specific collection
    public string GetNextSequenceValue(string sequenceName)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("_id", sequenceName);
        var update = Builders<BsonDocument>.Update.Inc("sequence_value", 1);
        var options = new FindOneAndUpdateOptions<BsonDocument, BsonDocument>
        {
            IsUpsert = true, // Create the counter document if it doesn't exist
            ReturnDocument = ReturnDocument.After // Return the updated document
        };

        var updatedDocument = Counters.FindOneAndUpdate(filter, update, options);
        return updatedDocument["sequence_value"].ToString()!;
    }
}
