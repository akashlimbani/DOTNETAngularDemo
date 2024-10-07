using BackendAPI.Models.Entity;

namespace BackendAPI.Services;

public interface IUsersService
{
    Task<List<User>> GetUsersAsync();
    Task<User> GetUserByIdAsync(string id);
    Task<User> CreateUserAsync(User user);
    Task DeleteUserAsync(string id);
}
