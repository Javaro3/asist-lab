using Common.Domains;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implementation;

public class TripRepository : Repository<Trip>, ITripRepository
{
    public TripRepository(TripContext context) : base(context) {}
    
    public async Task<List<Trip>> GetMyAsync(int userId)
    {
        return await _dbSet
            .Include(e => e.Points)
            .Include(e => e.Images)
            .Include(e => e.Comments)
                .ThenInclude(e => e.User)
            .Where(e => e.UserId == userId && !e.IsFinish)
            .ToListAsync();
    }

    public async Task<List<Trip>> GetMyHistoryAsync(int userId)
    {
        return await _dbSet
            .Include(e => e.Points)
            .Include(e => e.Images)
            .Include(e => e.Comments)
                .ThenInclude(e => e.User)
            .Where(e => e.UserId == userId && e.IsFinish)
            .ToListAsync();
    }
    
    public async Task<List<Trip>> GetFriendTripsAsync(int userId)
    {
        var friendIds = _context.Friends
            .Where(e => e.SourceUserId == userId)
            .Select(e => e.TargetUserId);

        return await _dbSet
            .Include(e => e.Points)
            .Include(e => e.Images)
            .Include(e => e.Comments)
                .ThenInclude(e => e.User)
            .Where(e => friendIds.Contains(e.UserId))
            .ToListAsync();
    }

    public async Task UpdateAll(Trip model)
    {
        var oldPoints = _context.Points.Where(e => e.TripId == model.Id);
        _context.Points.RemoveRange(oldPoints);
        var oldImages = _context.Images.Where(e => e.TripId == model.Id);
        _context.Images.RemoveRange(oldImages);

        await UpdateAsync(model);
        await _context.SaveChangesAsync();
    }
}