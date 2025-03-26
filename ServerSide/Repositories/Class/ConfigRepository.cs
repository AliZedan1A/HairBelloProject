using Domain.DatabaseModels;
using ServerSide.Database;

namespace ServerSide.Repositories.Class
{
    public class ConfigRepository:Repository<AppConfigModel>
    {
        private readonly ApplicationDbContext _context;

        public ConfigRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
