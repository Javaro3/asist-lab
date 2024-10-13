using Common.Domains;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implementation;

public class ImageRepository : Repository<Image>, IImageRepository
{
    public ImageRepository(TripContext context) : base(context) {}
}