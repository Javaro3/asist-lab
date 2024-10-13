namespace Common.Domains;

public class Friend
{
    public int Id { get; set; }

    public int SourceUserId { get; set; }
    
    public User? SourceUser { get; set; }
    
    public int TargetUserId { get; set; }
    
    public User? TargetUser { get; set; }
}