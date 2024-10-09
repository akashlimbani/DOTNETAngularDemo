using BackendAPI.Data;
using BackendAPI.Models.Entity;
using MongoDB.Driver;

namespace BackendAPI.Services;

public class UsersService : IUsersService
{
    private readonly IMongoCollection<User> _users;
    private readonly MongoDBContext _dbContext;

    public UsersService(MongoDBContext context)
    {
        _dbContext = context;
        _users = _dbContext.Users;
    }

    // Get all users
    public async Task<List<User>> GetUsersAsync() =>
        await _users.Find(user => true).ToListAsync();

    // Get a user by ID
    public async Task<User> GetUserByIdAsync(string id) =>
        await _users.Find(user => user.Id == id).FirstOrDefaultAsync();

    // Create a new user with auto-incremented ID
    public async Task<User> CreateUserAsync(User user)
    {
        // Auto-increment ID
        user.Id = _dbContext.GetNextSequenceValue("userId");
        user.CreatedAt = DateTime.UtcNow;
        await _users.InsertOneAsync(user);
        return user;
    }

    // Delete a user by ID
    public async Task DeleteUserAsync(string id) =>
        await _users.DeleteOneAsync(user => user.Id == id);

    public async Task UpdateUserAsync(User user) => await _users.ReplaceOneAsync(u => u.Id == user.Id, user);
}

