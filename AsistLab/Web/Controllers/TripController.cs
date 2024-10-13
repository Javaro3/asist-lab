using Common.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DataServices;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class TripController : Controller
{
    private readonly TripDataService _tripDataService;

    public TripController(TripDataService tripDataService)
    {
        _tripDataService = tripDataService;
    }
    
    [HttpPost("put")]
    public async Task<IActionResult> Put([FromForm] EditTripDto dto)
    {
        if (int.TryParse(User.FindFirst("id")?.Value, out var userId))
            dto.UserId = userId;

        try
        {
            await _tripDataService.PutTripAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }
    
    [HttpGet("get-my")]
    public async Task<IActionResult> GetMy()
    {
        try
        {
            if (int.TryParse(User.FindFirst("id")?.Value, out var userId))
            {
                var dtos = await _tripDataService.GetMyAsync(userId);
                return Ok(dtos);
            }
            return BadRequest(new { Message = "Server problem" });
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }
    
    [HttpGet("get-history")]
    public async Task<IActionResult> GetHistory()
    {
        try
        {
            if (int.TryParse(User.FindFirst("id")?.Value, out var userId))
            {
                var dtos = await _tripDataService.GetHistoryAsync(userId);
                return Ok(dtos);
            }
            return BadRequest(new { Message = "Server problem" });
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }
    
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _tripDataService.Delete(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }
    
    [HttpPost("start-trip")]
    public async Task<IActionResult> StartTrip([FromBody] int tripId)
    {
        try
        {
            await _tripDataService.StartTrip(tripId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }
    
    [HttpPost("end-trip")]
    public async Task<IActionResult> EndTrip([FromBody] int tripId)
    {
        try
        {
            await _tripDataService.EndTrip(tripId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }
    
    [HttpGet("get-friend-trips")]
    public async Task<IActionResult> GetFriendTrips()
    {
        try
        {
            if (int.TryParse(User.FindFirst("id")?.Value, out var userId))
            {
                var dtos = await _tripDataService.GetFriendTripsAsync(userId);
                return Ok(dtos);
            }
            return BadRequest(new { Message = "Server problem" });
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }
}