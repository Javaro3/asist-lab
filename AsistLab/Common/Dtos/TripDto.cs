namespace Common.Dtos;

public class TripDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
   
    public bool IsLaunched { get; set; }
    
    public DateTime ExpectedStartTime { get; set; }
    
    public DateTime ExpectedFinishTime { get; set; }
    
    public DateTime? RealStartTime { get; set; }
    
    public DateTime? RealFinishTime { get; set; }
    
    public TimeSpan? Duration { get; set; }
    
    public ICollection<PointDto> Points { get; set; } = [];
    
    public ICollection<ImageDto> Images { get; set; } = [];
    
    public ICollection<CommentDto> Comments { get; set; } = [];
}