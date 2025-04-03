using Domain.DatabaseModels;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using ServerSide.Database;


namespace ServerSide.Repositories.Class
{
    public class UserRepository : Repository<UserModel>
    {
        private readonly ApplicationDbContext _context;

        public async Task<ServiceReturnModel<bool>> DeleteUser(string phon)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phon);
            if(user is null) return new ServiceReturnModel<bool>(){
                IsSucceeded = false,
                Comment = "المستخدم غير موجود"
            }; 
            var Service =  await DeleteAsync(user.Id);
            if (Service.IsSucceeded)
            {
                return new()
                {
                    IsSucceeded = true,

                };
            }
            else
            {
                return new()
                {
                    IsSucceeded = false,
                    Comment = Service.Comment

                };
            }
        }

        public async Task<ServiceReturnModel<UserModel>> GetUserByPhonNumber(string PhonNumber)
        {
            var user = await _context.Users.Where(x => x.PhoneNumber == PhonNumber).FirstOrDefaultAsync();
            return user is not null ? new ServiceReturnModel<UserModel>() { IsSucceeded = true, Value = user }
            : new() { IsSucceeded = false, Comment = "المستخدم غير موجود " }; 
        }
        public async Task<ServiceReturnModel<bool>> ToogleBan(string PhonNumber)
        {
            var user =   await _context.Users.Where(x=>x.PhoneNumber == PhonNumber).FirstOrDefaultAsync();
            user.IsBanned = !user.IsBanned;
            var model = await UpdateAsync(user);

            return model.IsSucceeded  ? new ServiceReturnModel<bool> { IsSucceeded = true } : new ServiceReturnModel<bool> { IsSucceeded = false , Comment ="فشل حظر المستخدم" };
        }
        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
