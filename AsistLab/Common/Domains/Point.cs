namespace Common.Domains;

public class Point
{
    public int Id { get; set; }
    
    public int TripId { get; set; }
    
    public Trip? Trip { get; set; }

    public double Latitude { get; set; }
    
    public double Longitude { get; set; }
    
    public int Order { get; set; }
}