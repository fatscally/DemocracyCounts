using Microsoft.AspNetCore.Mvc;
using UserManagement.Api.Dtos;
using UserManagement.Api.Interfaces;

namespace UserManagement.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAll()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("active")]
    public async Task<ActionResult<List<UserDto>>> GetActive()
    {
        var users = await _userService.GetActiveUsersAsync();
        return Ok(users);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateUserStatusDto dto)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound();

        var success = await _userService.UpdateUserStatusAsync(id, dto.Active);
        if (!success)
        {
            if (!dto.Active && user.Roles.Contains("Admin"))
                return BadRequest("Cannot disable administrator accounts.");
            return BadRequest("Failed to update user status.");
        }

        return NoContent();
    }
}