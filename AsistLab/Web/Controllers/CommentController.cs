using Common.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DataServices;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CommentController : Controller
{
    private readonly CommentDataService _commentDataService;

    public CommentController(CommentDataService commentDataService)
    {
        _commentDataService = commentDataService;
    }

    [HttpPost("add-comment")]
    public async Task<IActionResult> AddComment([FromBody] CommentDto dto)
    {
        if (int.TryParse(User.FindFirst("id")?.Value, out var userId))
        {
            dto.UserId = userId;
            var comment = await _commentDataService.AddCommentAsync(dto);
            return Ok(comment);
        }
        return BadRequest();
    }
}