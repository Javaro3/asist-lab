using Microsoft.AspNetCore.Http;

namespace Common.Dtos;

public class EditTripDto
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public int UserId { get; set; }

    public DateTime ExpectedStartTime { get; set; }
    
    public DateTime ExpectedFinishTime { get; set; }
    
    public ICollection<PointDto> Points { get; set; } = [];
    
    public ICollection<IFormFile> Images { get; set; } = [];
}