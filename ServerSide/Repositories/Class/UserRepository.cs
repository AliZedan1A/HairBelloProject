using Domain.DatabaseModels;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using ServerSide.Database;
using Twilio.TwiML.Voice;

namespace ServerSide.Repositories.Class
{
    public class UserRepository : Repository<UserModel>
    {
        private readonly ApplicationDbContext _context;

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
