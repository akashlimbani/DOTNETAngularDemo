using BackendAPI.Models.Entity;
using BackendAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUsersService usersService) : ControllerBase
{
    private readonly IUsersService _usersService = usersService;

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _usersService.GetUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var user = await _usersService.GetUserByIdAsync(id);
        if (user == null) return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        if (!user.ConsentGiven)
            return BadRequest("User consent is required.");

        var createdUser = await _usersService.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        await _usersService.DeleteUserAsync(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] User user)
    {
        if (!user.ConsentGiven)
            return BadRequest("User consent is required.");

        var existingUser = await _usersService.GetUserByIdAsync(id);
        if (existingUser == null)
            return NotFound("User not found.");

        existingUser.Name = user.Name;
        existingUser.Email = user.Email;
        existingUser.ConsentGiven = user.ConsentGiven;

        await _usersService.UpdateUserAsync(existingUser);

        return NoContent(); // 204 No Content response
    }
}
