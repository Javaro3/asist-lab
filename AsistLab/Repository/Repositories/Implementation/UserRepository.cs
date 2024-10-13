using Common.Domains;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implementation;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(TripContext context) : base(context) { }

    public async Task<User?> GetByLoginAsync(string login)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.Login == login);
    }

    public async Task<bool> UserWithThisLoginExistsAsync(string login)
    {
        return await _dbSet.AnyAsync(e => e.Login == login);
    }
}