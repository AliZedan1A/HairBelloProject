using Domain.DatabaseModels;
using Domain.DataTransfareObject;
using Domain.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerSide.Repositories;
using ServerSide.Repositories.Class;
using ServerSide.Services;
using System.Text.RegularExpressions;

namespace ServerSide.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    [RequiredKey]

    public class UsersController : ControllerBase
    {
        private readonly ConfigRepository _configrepo;
        private readonly UserRepository _repository;
        private readonly PhonSenderService _phonsender;
        public UsersController(UserRepository repository, PhonSenderService phonSender, ConfigRepository configrepo)
        {
            _repository = repository;
            _phonsender = phonSender;
            _configrepo = configrepo;
        }
        [HttpDelete("DeleteAccount")]
        public async Task<IActionResult> DeleteAccount([FromQuery] string PhonNumber)
        {
           var result = await  _repository.DeleteUser(PhonNumber);
            if (result.IsSucceeded)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpGet("IsCodeAvailble")]
        public async Task<IActionResult> IsCodeAvailble([FromQuery] string PhonNumber,[FromQuery]string Code)
        {

            var model = await _phonsender.CheckCode(PhonNumber,Code);
            return model.IsSucceeded ? Ok(model) : BadRequest(model);
        }
        [HttpGet("SendVerfy")]
        public async Task<IActionResult> SendVerfy([FromQuery] string PhonNumber,[FromQuery]string UserName)
        {

            var model = await _phonsender.SendVerfyCode(PhonNumber, UserName);
            return model.IsSucceeded ? Ok(model) : BadRequest(model);
            //return BadRequest(new ServiceReturnModel<bool>()
            //{
            //    IsSucceeded =false,
            //    Comment = "قمنا بتعطيل خاصية ارسال كود جديد حتى نشر التطبيق ! |اٍذا كنت من مراجعين البرنامج - استخدم الكود السابق المرفق"
            //});
        }
        [HttpGet("GetAppStatus")]
        public async Task<IActionResult> GetAppStatus()
        {
            var model = await _configrepo.GetAsync(1);
            return model.IsSucceeded ? Ok(model) : BadRequest(model);
        }

        [HttpGet("GetUserByPhonNumber")]
        public async Task<IActionResult> GetByPhonNumber([FromQuery] string PhonNumber)
        {
            var model = await _repository.GetUserByPhonNumber(PhonNumber);
            return model.IsSucceeded ? Ok(model) : BadRequest(model);
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
        public async Task<IActionResult> Create([FromBody] CreateUserDto model)
        {
            var result = await _repository.InsertAsync(new() { 
            IsBanned = false,
            Name = model.Name,
            Code = "9999876466",
            
            PhoneNumber = model.PhoneNumber,
            });
            return result.IsSucceeded ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserDto model)
        {
            var result = await _repository.UpdateAsync(new()
            {
                Id = model.Id,
                IsBanned =model.IsBanned,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
            });
            return result.IsSucceeded ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repository.DeleteAsync(id);
            return result.IsSucceeded ? Ok(result) : NotFound(result);
        }
        [HttpPost("ToogleBan")]
        public async Task<IActionResult> ToogleBan([FromBody] ToogleBanDto req)
        {
           var admin =  await _configrepo.GetAsync(1);
            if (!(admin.IsSucceeded&&admin.Value.AdminPhonNumber ==req.LocalPhonNumber))
            {
                return Unauthorized(new ServiceReturnModel<bool>()
                {
                    IsSucceeded = false,
                    Comment="لا تملك صلاحيات ل استخدام هذا الامر"
                });
            }

            
          var model =   await _repository.ToogleBan(req.BanPhonNumber);
            return model.IsSucceeded? Ok(model) : BadRequest(model);
        }
        [HttpPost("ToogleApp")]
        public async Task<IActionResult> ToogleApp([FromBody] ToogleAppDto req)
        {
            var admin = await _configrepo.GetAsync(1);
            if (!(admin.IsSucceeded && admin.Value.AdminPhonNumber == req.LocalPhonNumber))
            {
                return Unauthorized(new ServiceReturnModel<bool>()
                {
                    IsSucceeded = false,
                    Comment = "لا تملك صلاحيات ل استخدام هذا الامر"
                });
            }
            admin.Value.IsHairBelloWork = !admin.Value.IsHairBelloWork;
            var ret = await _configrepo.UpdateAsync(admin.Value);

            return ret.IsSucceeded ? Ok(ret) : BadRequest(ret);
        }
    }
}
