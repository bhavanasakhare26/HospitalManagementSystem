using HospitalManagementSystem.Application.DTOs;
using HospitalManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var token = await _authService.LoginAsync(loginDto.Email, loginDto.Password);
        if (token == null) return Unauthorized("Invalid email or password");
        return Ok(token);
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var token = await _authService.RegisterAsync(registerDto.Email, registerDto.Password, registerDto.Role);
        return Ok(token);
    }
}
