using Domain.DatabaseModels;
using ServerSide.Database;

namespace ServerSide.Repositories.Class
{
    public class OrderServiceRepository : Repository<OrderServiceModel>
    {
        private readonly ApplicationDbContext _context;

        public OrderServiceRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
