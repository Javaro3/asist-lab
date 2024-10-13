using Common.Domains;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implementation;

public class FriendRepository : Repository<Friend>, IFriendRepository
{
    public FriendRepository(TripContext context) : base(context) {}
}