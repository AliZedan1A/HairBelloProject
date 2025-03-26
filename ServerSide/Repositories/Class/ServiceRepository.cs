using Domain.DatabaseModels;
using ServerSide.Database;

namespace ServerSide.Repositories.Class
{
    public class ServiceRepository : Repository<ServiceModel>
    {
        private readonly ApplicationDbContext _context;

        public ServiceRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
