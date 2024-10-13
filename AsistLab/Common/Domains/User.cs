namespace Common.Domains;

public class User
{
    public int Id { get; set; }

    public string Login { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;
    
    public DateTime CreatedOn { get; set; }

    public ICollection<Trip> Trips { get; set; } = [];
    
    public ICollection<Comment> Comments { get; set; } = [];
}