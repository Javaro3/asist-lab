using Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using Service.DataServices;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly UserDataService _userDataService;

    public AuthController(UserDataService userDataService)
    {
        _userDataService = userDataService;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        try
        {
            var token = await _userDataService.LoginAsync(dto);
            return Ok(new { Token = token });
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        try
        {
            var token = await _userDataService.RegisterAsync(dto);
            return Ok(new { Token = token });
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }
}