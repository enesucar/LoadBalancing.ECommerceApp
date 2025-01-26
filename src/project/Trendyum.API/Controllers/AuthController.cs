using Microsoft.AspNetCore.Mvc;
using Trendyum.Application.Interfaces.Auth;
using Trendyum.Common.Models.Auth;

namespace Trendyum.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(UserLoginRequest request)
    {
        var result = await _authService.LoginAsync(request);
        if (result.Result == "Succeeded")
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(UserRegisterRequest request)
    {
        var result = await _authService.RegisterAsync(request);
        if (result.Succeeded)
        {
            return Created("", result);
        }
        return BadRequest(result);
    }
}
