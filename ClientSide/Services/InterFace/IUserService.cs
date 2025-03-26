

using Domain.DatabaseModels;
using Domain.Models;
using Domain.DataTransfareObject;

namespace ClientSide.Services.InterFace
{
    interface IUserService
    {
        public Task<ServiceReturnModel<bool>> ToogleBan(ToogleBanDto PhonNumber);
        public Task<ServiceReturnModel<List<UserModel>>> GetUsers();
        public Task<ServiceReturnModel<UserModel>> GetUserByPhonNumber(string PhonNumber);
        public Task<ServiceReturnModel<bool>> ToogleApp(ToogleAppDto req);
        public Task<ServiceReturnModel<AppConfigModel>> GetConfig();
        public Task<ServiceReturnModel<bool>> InsertUser(CreateUserDto req);
        public Task<ServiceReturnModel<bool>> CheckCode(string PhonNumber, string code);
        public Task<ServiceReturnModel<bool>> SendValidtion(string PhonNumber,string UserName);
    }
}
