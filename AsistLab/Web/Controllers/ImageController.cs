using Microsoft.AspNetCore.Mvc;
using Service.DataServices;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageController : Controller
{
    private readonly ImageDataService _imageDataService;

    public ImageController(ImageDataService imageDataService)
    {
        _imageDataService = imageDataService;
    }
    
    [HttpGet("get")]
    public async Task<IActionResult> GetImage(int id)
    {
        var imageBytes = await _imageDataService.GetImageAsync(id);
        return File(imageBytes, "image/jpeg"); 
    }
}