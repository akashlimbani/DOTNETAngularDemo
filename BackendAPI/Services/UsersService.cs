using BackendAPI.Data;
using BackendAPI.Models.Entity;
using MongoDB.Driver;

namespace BackendAPI.Services;

public class UsersService : IUsersService
{
    private readonly IMongoCollection<User> _users;

    public UsersService(MongoDBContext context)
    {
        _users = context.Users;
    }

    public async Task<List<User>> GetUsersAsync() =>
        await _users.Find(user => true).ToListAsync();

    public async Task<User> GetUserByIdAsync(string id) =>
        await _users.Find(user => user.Id == id).FirstOrDefaultAsync();

    public async Task<User> CreateUserAsync(User user)
    {
        user.CreatedAt = DateTime.UtcNow;
        await _users.InsertOneAsync(user);
        return user;
    }

    public async Task DeleteUserAsync(string id) =>
        await _users.DeleteOneAsync(user => user.Id == id);
}
