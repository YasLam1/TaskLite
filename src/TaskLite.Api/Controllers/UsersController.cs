using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskLite.Application.DTOs.Users;
using TaskLite.Domain.Entities;

namespace TaskLite.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    public UsersController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest req)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return NotFound();

        user.FullName = req.FullName;
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(new UserResponse
        {
            Id = user.Id,
            Email = user.Email!,
            FullName = user.FullName
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return NotFound();

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return NoContent();
    }

}
