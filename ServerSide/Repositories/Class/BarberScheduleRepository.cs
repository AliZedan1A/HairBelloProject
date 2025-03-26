using Domain.DatabaseModels;
using Domain.DataTransfareObject;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using ServerSide.Database;
using ServerSide.Services;

namespace ServerSide.Repositories.Class
{
    public class BarberScheduleRepository : Repository<BarberScheduleModel>
    {
        private readonly ApplicationDbContext _context;
        private readonly DateTimeService _datetimeService;
        public BarberScheduleRepository(ApplicationDbContext context , DateTimeService dateTimeService) : base(context)
        {
            _datetimeService =dateTimeService;
            _context = context;
        }
        public async Task<ServiceReturnModel<bool>> IsSheduleCanSet(CreateBarberScheduleDto req)
        {
            try
            {
                if (req == null)
                {
                    return new ServiceReturnModel<bool>
                    {
                        Value = false,
                        IsSucceeded = false,
                        Comment = "البيانات المُرسلة فارغة."
                    };
                }

                if (req.BarberId <= 0 || string.IsNullOrWhiteSpace(req.DayName))
                {
                    return new ServiceReturnModel<bool>
                    {
                        Value = false,
                        IsSucceeded = false,
                        Comment = "BarberId أو DayName غير صالحين."
                    };
                }

                if (req.StartHour >= req.EndHour)
                {
                    return new ServiceReturnModel<bool>
                    {
                        Value = false,
                        IsSucceeded = false,
                        Comment = "StartHour يجب أن يكون أقل من EndHour."
                    };
                }

                var existingSchedules = await _context.BarberSchedules
                    .Where(x => x.DayName == req.DayName && x.BarberId == req.BarberId)
                    .ToListAsync();

                foreach (var schedule in existingSchedules)
                {
                    bool isOverlapping = req.StartHour < schedule.EndHour &&
                                         req.EndHour > schedule.StartHour;

                    if (isOverlapping)
                    {
                        return new ServiceReturnModel<bool>
                        {
                            Value = false,
                            IsSucceeded = false,
                            Comment = "هناك تداخل مع شفت آخر موجود."
                        };
                    }
                }

                return new ServiceReturnModel<bool>
                {
                    Value = true,
                    IsSucceeded = true,
                    Comment = "يمكن تعيين الشفت بنجاح."
                };
            }
            catch (Exception ex)
            {
                return new ServiceReturnModel<bool>
                {
                    Value = false,
                    IsSucceeded = false,
                    Comment = $"حدث خطأ: {ex.Message}"
                };
            }
        }
        public async Task<ServiceReturnModel<List<AvailableSlotModel>>> GetAvailableSlots(
       int barberId, string dayName, DateTime date, List<int> serviceIds)
        {
            try
            {
                if (barberId <= 0 || string.IsNullOrWhiteSpace(dayName) || serviceIds == null || serviceIds.Count == 0)
                    return new ServiceReturnModel<List<AvailableSlotModel>>
                    {
                        Value = null,
                        IsSucceeded = false,
                        Comment = "Invalid input parameters."
                    };

                var schedules = await _context.BarberSchedules
                    .Where(s => s.BarberId == barberId && s.DayName == dayName)
                    .ToListAsync();

                if (schedules == null || schedules.Count == 0)
                {
                    return new ServiceReturnModel<List<AvailableSlotModel>>
                    {
                        Value = null,
                        IsSucceeded = false,
                        Comment = "No schedule found for this barber on this day."
                    };
                }

                var totalDuration = await _context.Services
                    .Where(s => serviceIds.Contains(s.Id) && s.BarberId == barberId)
                    .SumAsync(s => s.Time);

                if (totalDuration <= 0)
                    return new ServiceReturnModel<List<AvailableSlotModel>>
                    {
                        Value = null,
                        IsSucceeded = false,
                        Comment = "Selected services are invalid."
                    };

                var requiredTime = TimeSpan.FromMinutes(totalDuration);

                var bookedAppointments = await _context.Orders
                    .Where(o => o.BarberId == barberId && o.Date.Date == date.Date && o.IsCansled == false)
                    .OrderBy(o => o.FromTime)
                    .ToListAsync();

                var availableSlots = new List<AvailableSlotModel>();

                foreach (var schedule in schedules)
                {
                    TimeSpan currentStart = schedule.StartHour;

                    while (currentStart + requiredTime <= schedule.EndHour)
                    {
                        bool isConflicting = bookedAppointments.Any(a =>
                            currentStart < a.ToTime.TimeOfDay &&
                            (currentStart + requiredTime) > a.FromTime.TimeOfDay
                            );

                        bool IsGoning = (date.Date == _datetimeService.IsraelNow().Date) && currentStart < _datetimeService.IsraelNow().TimeOfDay;
                        if (!isConflicting && !IsGoning)
                        {
                            availableSlots.Add(new AvailableSlotModel
                            {
                                Start = currentStart.ToString(@"hh\:mm"),
                                End = (currentStart + requiredTime).ToString(@"hh\:mm")
                            });
                        }

                        currentStart += requiredTime;
                    }
                }

                if (!availableSlots.Any())
                {
                    return new ServiceReturnModel<List<AvailableSlotModel>>
                    {
                        Value = null,
                        IsSucceeded = false,
                        Comment = "No available slots found."
                    };
                }

                return new ServiceReturnModel<List<AvailableSlotModel>>
                {
                    Value = availableSlots,
                    IsSucceeded = true,
                    Comment = "Available slots retrieved successfully."
                };
            }
            catch (Exception ex)
            {
                return new ServiceReturnModel<List<AvailableSlotModel>>
                {
                    Value = null,
                    IsSucceeded = false,
                    Comment = $"An error occurred: {ex.Message}"
                };
            }
        }


        public async Task<ServiceReturnModel<BarberScheduleModel>> GetBarberScheduleById(int barberId, string dayName)
        {
            try
            {
                var schedule = await _context.BarberSchedules
                    .FirstOrDefaultAsync(s => s.BarberId == barberId && s.DayName == dayName);

                if (schedule == null)
                {
                    return new ServiceReturnModel<BarberScheduleModel>() { IsSucceeded = false, Comment = "\"No shifts found for the selected day.\"" };
                }

                return new ServiceReturnModel<BarberScheduleModel>() { IsSucceeded =true,Value = schedule};
            }catch (Exception ex) {

                return new ServiceReturnModel<BarberScheduleModel>() { IsSucceeded = false, Comment = ex.Message };

            }
        }
    }
}
