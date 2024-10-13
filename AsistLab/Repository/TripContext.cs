using Common.Domains;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class TripContext : DbContext
{
    public DbSet<Comment> Comments { get; set; }
    
    public DbSet<Friend> Friends { get; set; }
    
    public DbSet<Image> Images { get; set; }
    
    public DbSet<Point> Points { get; set; }
    
    public DbSet<Trip> Trips { get; set; }
    
    public DbSet<User> Users { get; set; }
    
    public TripContext(DbContextOptions<TripContext> options) : base(options) { }
}