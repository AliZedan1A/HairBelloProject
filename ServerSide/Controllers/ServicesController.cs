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

    public class ServicesController : ControllerBase
    {
        private readonly ServiceRepository _repository;

        public ServicesController(ServiceRepository repository)
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
        public async Task<IActionResult> Create([FromBody] CreateServiceDto model)
        {

            var result = await _repository.InsertAsync(new() { 
            BarberId = model.BarberId,
            Price = model.Price,
            Time = model.Time,
            Title = model.Title,    
            });
            return result.IsSucceeded ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateServiceDto model)
        {
            var res = await _repository.GetAsync(model.Id);
            if (res.IsSucceeded)
            {
                res.Value.Title = model.Title;
                res.Value.Price = model.Price;
                res.Value.Time = model.Time;

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
