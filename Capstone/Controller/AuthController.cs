namespace Capstone.Controller;

using Capstone.DTOs;
using Capstone.Entities;
using Capstone.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;
    public AuthController(IAuthService _authService)
    {
        this.authService = _authService;
    }

    [HttpPost("login", Name = "Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto request)
    {
        var token = await authService.LoginAsync(request);
        if (token == null)
        {
            return Unauthorized(new { message = "Invalid email or password." });
        }
        return Ok(new { token });
    }

    [HttpPost("register", Name = "Register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto request)
    {
        try
        {
            var token = await authService.RegisterAsync(request);
            return Ok(new { token });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

}