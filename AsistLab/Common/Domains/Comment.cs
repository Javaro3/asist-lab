namespace Common.Domains;

public class Comment
{
    public int Id { get; set; }

    public string Value { get; set; } = string.Empty;
    
    public int UserId { get; set; }
    
    public User? User { get; set; }
    
    public int TripId { get; set; }
    
    public Trip? Trip { get; set; }
}