using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DataServices;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UserController : Controller
{
    private readonly UserDataService _userDataService;

    public UserController(UserDataService userDataService)
    {
        _userDataService = userDataService;
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> Get()
    {
        var dtos = await _userDataService.GetAllAsync();
        return Ok(dtos);
    }
    
    [HttpPost("add-friend")]
    public async Task<IActionResult> AddFriend([FromBody] int friendId)
    {
        if (int.TryParse(User.FindFirst("id")?.Value, out var userId))
        {
            await _userDataService.AddFriend(userId, friendId);
            return Ok();
        }

        return BadRequest();
    }
}