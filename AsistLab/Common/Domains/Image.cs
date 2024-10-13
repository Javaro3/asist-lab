namespace Common.Domains;

public class Image
{
    public int Id { get; set; }
    
    public int TripId { get; set; }
    
    public Trip? Trip { get; set; }

    public string Url { get; set; } = string.Empty;
}