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
    public class PreviewImageController : ControllerBase
    {
        private readonly PreviewImageRepository _repository;

        public PreviewImageController(PreviewImageRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repository.GetImages();
            return result.IsSucceeded ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _repository.GetAsync(id);
            return result.IsSucceeded ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create( IFormFile file)
        {
            var result = await _repository.AddImage(file);
            return result.IsSucceeded ? Ok(result) : BadRequest(result);
        }
        [HttpGet("GetVideos")]
        public async Task<IActionResult> GetVideos()
        {
          var te = await  _repository.GetVideos();
            return te.IsSucceeded ? Ok(te) : BadRequest(te);
        }
        [HttpPost("UploadVideo")]
        public async Task<IActionResult> UploadVideo(IFormFile file)
        {
           var model =   await _repository.AddVideo(file);
           return model.IsSucceeded ? Ok(model) : BadRequest(model);
        }

        [HttpDelete("DeleteVideo/{id}")]
        public async Task<IActionResult> DeleteVideo(int id)
        {
            var result = await _repository.DeleteVideo(id);
            return result.IsSucceeded ? Ok(result) : NotFound(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repository.DeleteImage(id);
            return result.IsSucceeded ? Ok(result) : NotFound(result);
        }
    }
}
