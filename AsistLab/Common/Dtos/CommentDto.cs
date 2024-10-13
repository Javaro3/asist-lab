namespace Common.Dtos;

public class CommentDto
{
    public int Id { get; set; }

    public string Value { get; set; } = string.Empty;
    
    public int UserId { get; set; }
    
    public string UserLogin { get; set; } = string.Empty;
    
    public int TripId { get; set; }
}