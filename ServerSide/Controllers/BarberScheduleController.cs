using Domain.DatabaseModels;
using Domain.DataTransfareObject;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerSide.Repositories;
using ServerSide.Repositories.Class;
using ServerSide.Services;

namespace ServerSide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [RequiredKey]
    public class BarberScheduleController : ControllerBase
    {
        private readonly BarberScheduleRepository _repository;
        private readonly BarberRepository barberrepo;
        private readonly DateTimeService dateTimeService;

        public BarberScheduleController(BarberScheduleRepository repository ,BarberRepository _barberrepo, DateTimeService dateTimeService)
        {
            _repository = repository;
            barberrepo = _barberrepo;
            this.dateTimeService = dateTimeService;
        }
        [HttpGet("GetAvailbleSolts")]
        public async Task<ActionResult<ServiceReturnModel<List<AvailableSlotModel>>>> GetAvailbleSolts([FromQuery]int BarberId ,[FromQuery]string ServiceIds, [FromQuery]string DayName , [FromQuery]DateTime Date)
        {
            var list = ServiceIds
    .Split(',', StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .ToList();
            var model  = await barberrepo.GetAsync(BarberId);
            if (model.IsSucceeded)
            {
                if(model.Value.EndBreak < dateTimeService.IsraelNow() )
                {
                    model.Value.IsInBreak = false;
                    var res = await barberrepo.UpdateAsync(model.Value);
                    if (!res.IsSucceeded)
                    {
                        return BadRequest(new ServiceReturnModel<List<AvailableSlotModel>>()
                        {
                            Comment = "حصل خطأ أثناء تحديث حالة الحلاق",
                            IsSucceeded = false
                        });

                    }

                }
                if (model.Value.IsInBreak && model.Value.StartBreak < dateTimeService.IsraelNow()&& model.Value.EndBreak.Date > Date.Date)
                {
                    return BadRequest(new ServiceReturnModel<List<AvailableSlotModel>>() {
                    Comment = "هذا الحلاق  في استراحة  لا يمكنك حجز عنده",
                    IsSucceeded = false
                    });
                }
                var sh = await _repository.GetAvailableSlots(BarberId, DayName, Date, list);
                return sh.IsSucceeded ? Ok(sh) : BadRequest(sh);

            }
            else
            {
                return BadRequest(new ServiceReturnModel<List<AvailableSlotModel>>()
                {
                    Comment = "لم يتم ايجاد الحلاق",
                    IsSucceeded = false
                });

            }


        }
        [HttpGet("{barberId}/schedules/{dayName}")]
        public async Task<ActionResult<ServiceReturnModel<BarberScheduleModel>>> GetBarberSchedule(int barberId, string dayName)
        {
            var sh = await _repository.GetBarberScheduleById(barberId, dayName);
            return sh.IsSucceeded ? Ok(sh) : BadRequest(sh);

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return result.IsSucceeded ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _repository.GetAsync(id);
            return result.IsSucceeded ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBarberScheduleDto model)
        {

            var res = await _repository.IsSheduleCanSet(model);
            if (!res.IsSucceeded)  return BadRequest(res);
            var result = await _repository.InsertAsync(new()
            {
                BarberId = model.BarberId,
                DayName = model.DayName,
                StartHour   = model.StartHour,
                EndHour = model.EndHour,
                
            });
            return result.IsSucceeded ? Ok(result): BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBarberScheduleDto model)
        {
            var res = await _repository.GetAsync(model.Id);
            if(res.IsSucceeded)
            {
                res.Value.StartHour = model.StartHour;
                res.Value.EndHour = model.EndHour;
                res.Value.DayName = model.DayName;

                var result = await _repository.UpdateAsync(res.Value);
                
                return result.IsSucceeded ? Ok(result) : BadRequest(result);

            }
            else
            {
                return BadRequest(res);
            }
        }
       

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repository.DeleteAsync(id);
            return result.IsSucceeded ? Ok(result) : NotFound(result);
        }
    }
}
