using Microsoft.AspNetCore.Mvc;
using ModularNauka.Users.Application;

namespace ModularNauka.Users.Api;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var user = await _userService.RegisterAsync(request.Name, request.Email);
        return Ok(new { user.Id, user.Name, user.Email });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user is null) return NotFound();
        return Ok(user);
    }
}

public record RegisterRequest(string Name, string Email);