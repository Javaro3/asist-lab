using Common.Domains;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implementation;

public class CommentRepository : Repository<Comment>, ICommentRepository
{
    public CommentRepository(TripContext context) : base(context)
    {
    }

    public override async Task<Comment?> GetByIdAsync(int id)
    {
        return await _dbSet.Include(e => e.User).FirstOrDefaultAsync(e => e.Id == id);
    }
}