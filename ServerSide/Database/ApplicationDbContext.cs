using Domain.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using ServerSide.Services;

namespace ServerSide.Database
{
    public class ApplicationDbContext:DbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ) : base(options)
        {
        }
        public DbSet<BarberModel> Barbers { get; set; }
        public DbSet<BarberScheduleModel> BarberSchedules { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderServiceModel> OrderServices { get; set; }
        public DbSet<ServiceModel> Services { get; set; }
        public DbSet<PreviewImageModel> Images { get; set; }
        public DbSet<AppConfigModel> Config { get; set; }

    }
}
