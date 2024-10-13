using Common.Domains;

namespace Repository.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByLoginAsync(string login);
    
    Task<bool> UserWithThisLoginExistsAsync(string login);
}