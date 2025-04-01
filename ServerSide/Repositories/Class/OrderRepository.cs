using Domain.DatabaseModels;
using Domain.DataTransfareObject;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using ServerSide.Database;
using ServerSide.Services;

namespace ServerSide.Repositories.Class
{
    public class OrderRepository : Repository<OrderModel>
    {
        private readonly ApplicationDbContext _context;
        private readonly DateTimeService _datatimeservice;
        public OrderRepository(ApplicationDbContext context , DateTimeService dateTimeService) : base(context)
        {
            _context = context;
            _datatimeservice = dateTimeService;
        }
        public async Task<ServiceReturnModel<OrderModel>> GetOrderPhonnumber(string Phonnumber)
        {
            var model = await _context.Users.Where(x => x.PhoneNumber == Phonnumber).Include(x => x.Orders).FirstOrDefaultAsync();
            if (model == null)
            {
                return new()
                {
                    IsSucceeded = false,
                    Comment = "لم يتم التعرف على المستخدم في قواعد البيانات"
                };
            }
            var last = model.Orders.LastOrDefault();
            if (last == null)
            {
                return new()
                {
                    IsSucceeded = false,
                    Comment = "لا يوجد حجز"
                };
            }
            if (last.IsCansled)
            {
                return new()
                {
                    IsSucceeded = false,
                    Comment = "الحجز ملغي"
                };
            }

            if (_datatimeservice.IsraelNow().Date < last.Date.Date)
            {
                return new()
                {
                    IsSucceeded = true,
                    Value = last
                };

            
            }

            else if(_datatimeservice.IsraelNow().Date == last.Date.Date)
            {
                var nowTime = _datatimeservice.IsraelNow().TimeOfDay;
                if (nowTime <= last.FromTime.TimeOfDay)
                {
                    return new()
                    {
                        IsSucceeded = true,
                        Value = last
                    };
                }
                else
                {
                    return new()
                    {
                        IsSucceeded = false,
                        Comment = "لا يوجد حجز"
                    };
                }

            }
            else
            {
                return new()
                {
                    IsSucceeded = false,
                    Comment = "لا يوجد حجز في التاريخ"
                };
            }


        }
        public async Task<ServiceReturnModel<List<OrderModel>>> GetOrderRanged(DateTime from , DateTime to)
        {
            var orders = await _context.Orders.Where(x=>x.Date.Date >= from.Date && x.Date.Date <= to.Date).Include(x=>x.User).ToListAsync();
            if (!orders.Any())
            {
                return new()
                {
                    IsSucceeded = false,
                    Comment = "لا يوجد أي حجز في هذا الوقت"
                };

            }
            else
            {
                return new()
                {
                    IsSucceeded = true,
                    Value = orders
                };
            }
        }
        public async Task<ServiceReturnModel< List<OrderModel>>> GetOrderLimited(GetElementsLimtedDto req)
        {
            var selectedDay = req.date.Date;
            var ret = await _context.Orders
                .Where(x => x.BarberId == req.BarberId
                    && x.Date.Year == selectedDay.Year
                    && x.Date.Month == selectedDay.Month
                    && x.Date.Day == selectedDay.Day
                    
                    && x.IsCansled !=true
                )
                .Include(x => x.User).Include(x=>x.OrderServices)
                .Skip(req.Start)
                .Take(req.End )
                .OrderByDescending(x => x.Date).OrderBy(x=>x.FromTime)
                .ToListAsync();
            if(req.date.Date == _datatimeservice.IsraelNow().Date)
            {
                ret = ret.Where(x => x.ToTime.TimeOfDay >= _datatimeservice.IsraelNow().TimeOfDay).ToList();
            }
            if (ret.Count > 0|| ret is not null)
            {
                return new() { 
                IsSucceeded = true,
                Value = ret
                };
            }
            else
            {
                return new()
                {
                    IsSucceeded = false,
                    Comment = "لم يتم ايجاد اي اوردرات في هذا النطاق"
                };

            }

        }
        public async Task<ServiceReturnModel<bool>> CanUserMakeOrder(string PhonNumber)
        {
            var model = await _context.Users.Where(x=>x.PhoneNumber == PhonNumber ).Include(x=>x.Orders).FirstOrDefaultAsync();
            if(model == null)
            {
                return new()
                {
                    IsSucceeded = false,
                    Comment = "لم يتم التعرف على المستخدم في قواعد البيانات"
                };
            }
            if (model.IsBanned)
            {
                return new ServiceReturnModel<bool>() { IsSucceeded = false, Comment = "حسابك محظور , راجع مالك التطبيق" };
            }

            var last = model.Orders.LastOrDefault();
            if (last == null) {
                return new()
                {
                    IsSucceeded = true,
                    Comment = "لا يوجد حجز"
                };
            }
            if (last.IsCansled)
            {
                return new()
                {
                    IsSucceeded = true,
                    Comment = "الحجز ملغي"
                };
            }

            if (_datatimeservice.IsraelNow().Date > last.Date.Date)
            {
                return new()
                {
                    IsSucceeded = true,
                    Value = true
                };
                
            }
            else if(_datatimeservice.IsraelNow().Date == last.Date.Date)
            {
                var nowTime = _datatimeservice.IsraelNow().TimeOfDay;

                if (nowTime >= last.ToTime.TimeOfDay)
                {
                    return new()
                    {
                        IsSucceeded = true,
                        Value = true
                    };
                }
                else
                {
                    return new()
                    {
                        IsSucceeded = false,
                        Comment = "انت تملك حجز بالفعل"
                    };
                }
            }
            else
            {
                return new()
                {
                    IsSucceeded = false,
                    Comment = "انت تملك حجز بالفعل"
                };
            }
        }
        public async Task<bool> OrderTime(int barberId, DateTime fromtime, DateTime endtime, DateTime date)
        {
            return await _context.Orders
        .AnyAsync(o =>
            o.BarberId == barberId &&
            o.Date.Date == date.Date &&
            o.IsCansled == false &&
            fromtime.TimeOfDay == o.FromTime.TimeOfDay &&
            endtime.TimeOfDay == o.ToTime.TimeOfDay
        );
        }
        public async Task<bool> IsOrderTimeAvailble(int barberId,DateTime fromtime,DateTime endtime ,DateTime date )
        {

                return await _context.Orders
            .AnyAsync(o =>
                o.BarberId == barberId &&
                o.Date.Date == date.Date &&
                o.IsCansled == false &&
                fromtime.TimeOfDay < o.ToTime.TimeOfDay &&
                endtime.TimeOfDay > o.FromTime.TimeOfDay
            );
        }

    }
}
