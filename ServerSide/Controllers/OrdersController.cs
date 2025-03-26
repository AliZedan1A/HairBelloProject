using Domain.DatabaseModels;
using Domain.DataTransfareObject;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using ServerSide.Repositories.Class;
using ServerSide.Services;

namespace ServerSide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [RequiredKey]
    public class OrdersController : ControllerBase
    {
        private readonly OrderRepository _repository;
        private readonly UserRepository _userRepository;
        private readonly DateTimeService _dateService;

        public OrdersController(OrderRepository repository, DateTimeService timeService,UserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _dateService = timeService;
        }
        
        [HttpGet("GetPastOrders")]
        public async Task<IActionResult> GetPastOrders([FromQuery]DateTime from , [FromQuery] DateTime to)
        {
            var response = await _repository.GetOrderRanged(from,to);
            return response.IsSucceeded ? Ok(response) : BadRequest(response);

        }
        [HttpGet("GetOrderLimited")]
        public async Task<IActionResult> GetOrdersLimted([FromQuery] GetElementsLimtedDto req)
        {
            var response = await _repository.GetOrderLimited(req);
            return response.IsSucceeded ? Ok(response) : BadRequest(response);
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
        [HttpGet("GetLastOrder/{PhonNumber}")]
        public async Task<IActionResult> Get(string PhonNumber)
        {
            var result = await _repository.GetOrderPhonnumber(PhonNumber);
            return result.IsSucceeded ? Ok(result) : NotFound(result);
        }
        [HttpPut("CancelOrder")]
        public async Task<IActionResult> CancelOrder([FromBody]int OrderId)
        {
            
            var model = await _repository.GetAsync(OrderId);
            if (model.IsSucceeded)
            {
                if (model.Value.Date.Date == _dateService.IsraelNow().Date && model.Value.FromTime.TimeOfDay - _dateService.IsraelNow().TimeOfDay <= TimeSpan.FromHours(2))
                {
                    return BadRequest(new ServiceReturnModel<bool>()
                    {
                      Comment = "لا يمكنك الغاء الحجز قبل بساعتين من موعده",
                      IsSucceeded =false
                    }
                    );

                }

                model.Value.IsCansled = true;
                var retedit = await _repository.UpdateAsync(model.Value);
                return retedit.IsSucceeded ? Ok(retedit) : BadRequest(retedit);

            }
            else
            {
                return BadRequest(model); 
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto model)
        {
            var availble = await  _repository.IsOrderTimeAvailble(model.BarberId, model.FromTime, model.ToTime, model.Date);
            if(availble)
            {
                return BadRequest(new ServiceReturnModel<bool>()
                {
                    IsSucceeded = false,
                    Comment = "هذا الوقت تم حجزه الرجاء اختيار وقت اخر"

                });
            }

            var IsOrder = await _repository.CanUserMakeOrder(model.PhonNumber);
            if (!IsOrder.IsSucceeded)
            {
                return BadRequest(IsOrder);
            }
            var ret = await _userRepository.GetUserByPhonNumber(model.PhonNumber);
            
            if (ret.IsSucceeded)
            {
                var result = await _repository.InsertAsync(new()
                {
                    FromTime = model.FromTime,
                    TotalPrice = model.TotalPrice,
                    ToTime = model.ToTime,
                    UserId = ret.Value.Id,
                    Date = model.Date,
                    IsCansled = false,
                    BarberId = model.BarberId,
                   
                    OrderServices = model.Services.Select(x => new OrderServiceModel()
                    {
                        ServiceId = x.ServiceId,
                    }).ToList()
                });
                return result.IsSucceeded ? Ok(result) : BadRequest(result);
            }
            else
            {
                return Unauthorized(new ServiceReturnModel<bool>() { 
                IsSucceeded= false,
                Comment = ret.Comment   
                
                });

            }
           
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateOrderDto model)
        {
            var result = await _repository.UpdateAsync(new() {
                FromTime = model.FromTime,
                TotalPrice = model.TotalPrice,
                ToTime = model.ToTime,
                UserId = model.UserId,
                BarberId = model.BarberId,
                Id = model.Id
            });
            return result.IsSucceeded ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repository.DeleteAsync(id);
            return result.IsSucceeded ? Ok(result) : NotFound(result);
        }

    }
}
