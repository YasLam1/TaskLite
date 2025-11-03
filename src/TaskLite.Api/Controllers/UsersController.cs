using Microsoft.AspNetCore.Mvc;
using TaskLite.Application.DTOs.Users;
using TaskLite.Application.UseCases.Users;

namespace TaskLite.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly CreateUserHandler _createUserHandler;
    private readonly GetUserByIdHandler _getUserByIdHandler;

    public UsersController(CreateUserHandler createUserHandler, GetUserByIdHandler getUserByIdHandler)
    {
        _createUserHandler = createUserHandler;
        _getUserByIdHandler = getUserByIdHandler;
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
}
