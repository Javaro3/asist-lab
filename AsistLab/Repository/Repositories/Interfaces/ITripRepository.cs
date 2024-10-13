using Common.Domains;

namespace Repository.Repositories.Interfaces;

public interface ITripRepository : IRepository<Trip>
{
    Task<List<Trip>> GetMyAsync(int userId);
    
    Task<List<Trip>> GetMyHistoryAsync(int userId);

    Task<List<Trip>> GetFriendTripsAsync(int userId);
    
    Task UpdateAll(Trip model);
}