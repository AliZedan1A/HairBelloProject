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
    public class BarberController : ControllerBase
    {
        private readonly BarberRepository _repository;

        public BarberController(BarberRepository repository)
        {
            _repository = repository;
        }
        [HttpPut("RemoveBarberBreak")]
        public async Task<IActionResult> RemoveBarberBreak(RemoveBarberBreak req)
        {
            var model = await _repository.GetAsync(req.BarberId);
            if (model.IsSucceeded)
            {
                model.Value.IsInBreak = false;
                var Res = await _repository.UpdateAsync(model.Value);
                return Res.IsSucceeded ? Ok(Res) : BadRequest(Res);
            }
            else
            {
                return BadRequest(model);
            }

        }
        [HttpPut("SetBarberBreak")]
        public async Task<IActionResult> SetBarberBreak([FromBody]CreateBarberBreak req)
        {
            var model = await _repository.GetAsync(req.BarberId);
            if (model.IsSucceeded)
            {
                model.Value.StartBreak = req.StartBreak;
                model.Value.EndBreak = req.EndBreak;
                model.Value.IsInBreak = true;

                var Res =  await _repository.UpdateAsync(model.Value);
                return Res.IsSucceeded ? Ok(Res) : BadRequest(Res);
            }
            else
            {
                return BadRequest(model);
            }
        }
        [HttpGet("GetBarberServices/{id}")]
        public async Task<IActionResult> GetBarberServices(int id)
        {
            var result = await _repository.GetServicesByBarberId(id);
            return result.IsSucceeded ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repository.GetBarberWithLists();
            return result.IsSucceeded ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _repository.GetAsync(id);
            return result.IsSucceeded ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBarberDto model)
        {
            var result = await _repository.InsertAsync(new()
            {
                Name = model.Name,
                IsInBreak = false,
               
            });
            return result.IsSucceeded ?Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBarberDto model)
        {
            var result = await _repository.UpdateAsync(new()
            {
                Id=model.Id,
                Name = model.Name,
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
