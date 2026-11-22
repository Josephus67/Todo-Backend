using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using TodoApp.Providers;
namespace TodoApp.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IAuthProvider authProvider) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestModel model)
    {
        var result = await authProvider.RegisterAsync(model);
        if (result == null)
        {
            return BadRequest(new { message = "Registration failed." });
        }
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
    {
        var result = await authProvider.LoginAsync(model);
        if (result == null)
        {
            return Unauthorized(new { message = "Invalid email or password." });
        }
        return Ok(result);
    }
}
