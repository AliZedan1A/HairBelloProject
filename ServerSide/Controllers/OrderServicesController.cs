using Domain.DatabaseModels;
using Domain.DataTransfareObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerSide.Repositories;
using ServerSide.Repositories.Class;

namespace ServerSide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [RequiredKey]
    public class OrderServicesController : ControllerBase
    {
        private readonly OrderServiceRepository _repository;

        public OrderServicesController(OrderServiceRepository repository)
        {
            _repository = repository;
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
        public async Task<IActionResult> Create([FromBody] CreateOrderServiceDto model)
        {
            var result = await _repository.InsertAsync(new()
            {
                ServiceId = model.ServiceId,
            });
            return result.IsSucceeded ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateOrderServiceDto model)
        {
            var result = await _repository.UpdateAsync(new() {
                Id =model.Id,
                OrderId = model.OrderId,
                ServiceId = model.ServiceId,

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
