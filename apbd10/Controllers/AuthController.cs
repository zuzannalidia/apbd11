using apbd10.DTO;
using apbd10.Services;
using Microsoft.AspNetCore.Mvc;

namespace apbd10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDTO registerDTO)
    {
        var result = await _authService.RegisterAsync(registerDTO);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO loginDTO)
    {
        var token = await _authService.LoginAsync(loginDTO);
        return Ok(new { token });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] string token)
    {
        var newToken = await _authService.RefreshTokenAsync(token);
        return Ok(new { token = newToken });
    }
}
