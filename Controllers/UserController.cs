using Microsoft.AspNetCore.Mvc;
using MyBlog.API.DTOs;
using MyBlog.API.Helpers;
using MyBlog.API.MongoDB.Models;
using MyBlog.API.MongoDB.Services;

namespace MyBlog.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
  
    private readonly ILogger<UserController> _logger;
    private readonly IMongoDBService<User> _userService;

    public UserController(ILogger<UserController> logger, IMongoDBService<User> userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpGet]
    public async Task<IEnumerable<UserDTO>> GetUsersAsync()
    {
        var users = (await _userService.GetItemsAsync())
                        .Select(user => user.AsDTO());

        return users;
    }

    // GET /users/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetUserAsync(Guid id)
    {
        var user = await _userService.GetItemAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return user.AsDTO();
    }

    // POST /users
    [HttpPost]
    public async Task<ActionResult<UserDTO>> CreateUserAsync(UserCreateDTO userDTO)
    { 
        var user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = userDTO.FirstName,
            LastName = userDTO.LastName,
            Email = userDTO.Email,
            CreatedDate = DateTimeOffset.UtcNow
        };

        await _userService.CreateItemAsync(user);

        return CreatedAtAction(nameof(GetUserAsync), new { id = user.Id }, user.AsDTO());
    }

    // PUT /users/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUserAsync(Guid id, UserUpdateDTO userUpdateDTO)
    {
        var currentUser = await _userService.GetItemAsync(id); 

        if (currentUser is null)
        {
            return NotFound();
        }

        User updatedUser = currentUser with
        {
            FirstName = userUpdateDTO.FirstName,
            LastName = userUpdateDTO.LastName,
            Email = currentUser.Email
        };

        await _userService.UpdateItemAsync(updatedUser);

        return NoContent();
    }

    // DELETE /items/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUserAsync(Guid id)
    {
        var existingUser = await _userService.GetItemAsync(id);
        
        if (existingUser is null)
        {
            return NotFound();
        }

        await _userService.DeleteItemAsync(id);

        return NoContent();
    }
}
