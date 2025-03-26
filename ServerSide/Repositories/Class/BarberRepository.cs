using Domain.DatabaseModels;
using Domain.DataTransfareObject;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerSide.Database;

namespace ServerSide.Repositories.Class
{
    public class BarberRepository : Repository<BarberModel>
    {
        private readonly ApplicationDbContext _context;
        public BarberRepository(ApplicationDbContext context) : base(context)
        {
            _context =context;
        }

        public async Task<ServiceReturnModel<List<BarberModel>>> GetBarberWithLists()
        {
            var List = await _context.Barbers.Include(x=>x.Services).Include(x=>x.BarberSchedules).ToListAsync();
            return !(List.Count == 0) && !(List is null) ? new ServiceReturnModel<List<BarberModel>>()
            {
                IsSucceeded = true,
                Value = List
            } : new()
            {
                Comment = "لا يوجد خدمات لهذا الحلاق",
                IsSucceeded = false
            };
        }

        public async Task<ServiceReturnModel<List<ServiceModel>>> GetServicesByBarberId(int id)
        {
            var List =  await _context.Services.Where(x => x.BarberId == id).ToListAsync();
            return !(List.Count == 0) && !(List is null) ? new ServiceReturnModel<List<ServiceModel>>()
            {
                IsSucceeded = true,
                Value = List
            } : new()
            {
                Comment = "لا يوجد خدمات لهذا الحلاق",
                IsSucceeded = false
            };
        }
    }
}
