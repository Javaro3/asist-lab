namespace Common.Domains;

public class Trip
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    
    public User? User { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
   
    public bool IsLaunched { get; set; }
    
    public bool IsFinish { get; set; }
    
    public DateTime ExpectedStartTime { get; set; }
    
    public DateTime ExpectedFinishTime { get; set; }
    
    public DateTime? RealStartTime { get; set; }
    
    public DateTime? RealFinishTime { get; set; }

    public ICollection<Point> Points { get; set; } = [];
    
    public ICollection<Image> Images { get; set; } = [];
    
    public ICollection<Comment> Comments { get; set; } = [];
}