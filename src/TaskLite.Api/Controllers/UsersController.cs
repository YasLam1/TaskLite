using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskLite.Application.DTOs.Users;
using TaskLite.Application.UseCases.Users;

namespace TaskLite.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly CreateUserHandler _createUserHandler;
    private readonly GetUserByIdHandler _getUserByIdHandler;
    private readonly UpdateUserHandler _updateUserHandler;
    private readonly DeleteUserHandler _deleteUserHandler;

    public UsersController(CreateUserHandler createUserHandler, 
        GetUserByIdHandler getUserByIdHandler,
        UpdateUserHandler updateUserHandler,
        DeleteUserHandler deleteUserHandler)
    {
        _createUserHandler = createUserHandler;
        _getUserByIdHandler = getUserByIdHandler;
        _updateUserHandler = updateUserHandler;
        _deleteUserHandler = deleteUserHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest req, CancellationToken ct)
    {
        var user = await _createUserHandler.HandleAsync(req, ct);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var user = await _getUserByIdHandler.HandleAsync(new GetUserByIdRequest { Id = id }, ct);
        if (user is null)
            return NotFound();

        return Ok(user);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest req, CancellationToken ct)
    {
        var updated = await _updateUserHandler.HandleAsync(req, ct);
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _deleteUserHandler.HandleAsync(id, ct);
        return NoContent();
    }

}
