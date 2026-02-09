namespace Capstone.Controller;
using Capstone.Entities;
using Capstone.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    public readonly IuserService userService;
    public UserController(IuserService _userService)    {
        this.userService = _userService;
    }

    [HttpGet(Name = "GetUsers")]
    public async Task<IActionResult> GetUsers()
    {
        return Ok(await userService.GetAllUsersAsync());
    }

    [HttpGet("{id}", Name = "GetUserById")]
    public async Task<IActionResult> GetUserById(int id)
    {       var user = await userService.GetUserByIdAsync(id);
         if (user == null)
            {
                return NotFound(new { message = $"User with ID {id} not found." });
            }
            return Ok(user);
    }

    [HttpPost(Name = "CreateUser")]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        var createdUser = await userService.CreateUserAsync(user);
        return CreatedAtRoute("GetUserById", new { id = createdUser.Id },
            createdUser);
    }

    [HttpPut("{id}", Name = "UpdateUser")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
    {        if (id != user.Id)
        {
            return BadRequest(new { message = "ID in URL does not match ID in body." });
        }   
        var updatedUser = await userService.UpdateUserAsync(id, user);
        if (updatedUser == null)
        {
            return NotFound(new { message = $"User with ID {id} not found." });
        }
        return Ok(updatedUser);


    }

    [HttpDelete("{id}", Name = "DeleteUser")]
    public async Task<IActionResult> DeleteUser(int id)
    {        
        var success = await userService.DeleteUserAsync(id);
        if (!success)        {
            return NotFound(new { message = $"User with ID {id} not found." });
        }
        return NoContent();
    }
}