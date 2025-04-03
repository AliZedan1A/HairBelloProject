using Domain.Models;
using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using ServerSide.Database;
using ServerSide.Repositories.Class;
using Domain.DatabaseModels;
using System.Text.RegularExpressions;



namespace ServerSide.Services
{
    public class PhonSenderService
    {
        private readonly UserRepository _userRepository;
        private readonly OrderRepository _orderRepository;
        private readonly DateTimeService _dateService;
        private readonly BarberRepository barbers;
        private readonly WhatsappService _whisappService;
        public PhonSenderService(UserRepository userRepository,WhatsappService whatsappService,OrderRepository orderRepository, DateTimeService IsraelTime,BarberRepository barbers)
        {
            _userRepository = userRepository;
            _dateService=IsraelTime;
            this.barbers = barbers;
            _orderRepository = orderRepository;
            _whisappService = whatsappService;
        }
        public async Task<ServiceReturnModel<bool>> SendOrderCreated(DateTime Date , DateTime Time,string PhonNumber,int barberid)
        {
            string pattern = @"^(?:0)?(\d{2})(\d{3})(\d{4})$";
            string replacement = "$1-$2-$3";

            string result = Regex.Replace(PhonNumber, pattern, replacement);

            if (!Regex.IsMatch(PhonNumber, @"^\d{10}$"))
            {
                return new ServiceReturnModel<bool>()
                {
                    IsSucceeded = false,
                    Comment = "رقم الهاتف يجب أن يحتوي على 10 أرقام فقط"
                };

              
            }
            string datvald = $"{Date.Day}/{Date.Month}/{Date.Year}";
            string Message = @$"Hair Bello تم حجز دور عند 
 في تاريخ: {datvald}
 في تمام الساعة ال: {Time.TimeOfDay.ToString("")}";

            var model =  _whisappService.Send("+972-" + result, Message);
            //if (!model.IsSucceeded)
            //{
            //    return model;
            //}
           
            var Barver = await barbers.GetAsync(barberid);
            var user =await _userRepository.GetUserByPhonNumber(PhonNumber);
            if (Barver.IsSucceeded&&user.IsSucceeded)
            {
                string AdminMessage = @$"Hair Bello تم حجز دور في 
 في تاريخ: {Date.ToShortDateString()}
 في تمام الساعة ال: {Time.TimeOfDay.ToString("")}
عند الحلاق: {Barver.Value.Name}
رقم المستخدم : {PhonNumber}
اسم المستخدم : {user.Value.Name}

";
                var model1 = _whisappService.Send("+972-" + "50-446-6866", AdminMessage);
            }
            return new ServiceReturnModel<bool>()
            {
                IsSucceeded = true,
            };
        }
        public async Task<ServiceReturnModel<bool>> CheckCode(string phoneNumber , string code)
        {
            var model = await _userRepository.GetUserByPhonNumber(phoneNumber);
            if (model.IsSucceeded&&model.Value.Code == code)
            {
                return new ServiceReturnModel<bool>
                {
                    IsSucceeded = true
                 ,
                    Comment = ""
                };
            }
            else
            {
                return new ServiceReturnModel<bool>
                {
                    IsSucceeded = false
                 ,
                    Comment = "خطأ في الكود المدخل"
                };
            }
        }
        public  async Task SendReminder()
        {
            var model = await _orderRepository.GetAllAsync();
            if (model.IsSucceeded)
            {
                foreach (var item in model.Value)
                {
                    var user = await _userRepository.GetAsync(item.UserId);
                    if(user.IsSucceeded)
                    {
                        if (item.Date.Date == _dateService.IsraelNow().Date && !item.IsCansled)
                        {
                            var fromTimeOnly = item.FromTime.TimeOfDay;
                            var israelTimeOnly = _dateService.IsraelNow().TimeOfDay;

                            var trimmedFromTime = new TimeSpan(item.FromTime.Hour, item.FromTime.Minute, 0);
                            var trimmedIsraelTime = new TimeSpan(_dateService.IsraelNow().Hour, _dateService.IsraelNow().Minute, 0);

                            var timeDifference = (trimmedFromTime - trimmedIsraelTime).Duration();
                            var realDifference = (trimmedFromTime - trimmedIsraelTime);

                            if (timeDifference.TotalMinutes == 30&& realDifference > TimeSpan.Zero)
                            {
                                
                                try
                                {
                                    string message = @$"
تبقى 30 دقيقة على الحجز الخاص بك - الرجاء عدم التأخر  HairBello
";
                                    string pattern = @"^(?:0)?(\d{2})(\d{3})(\d{4})$";
                                    string replacement = "$1-$2-$3";
                                    string result = Regex.Replace(user.Value.PhoneNumber, pattern, replacement);

                                    var whattsapp = await _whisappService.Send("+972-" + result, message);
                                    


                                }
                                catch
                                {
                                    continue;
                                }


                            }
                        }
                    }
         
                   
                }
            }
        }
        public async Task<ServiceReturnModel<bool>> SendVerfyCode(string PhonNumber,string username)
        {
            string pattern = @"^(?:0)?(\d{2})(\d{3})(\d{4})$";
            string replacement = "$1-$2-$3";

            string result = Regex.Replace(PhonNumber, pattern, replacement);

            if (!Regex.IsMatch(PhonNumber, @"^\d{10}$"))
            {
                return new ServiceReturnModel<bool>()
                {
                    IsSucceeded = false,
                    Comment = "رقم الهاتف يجب أن يحتوي على 10 أرقام فقط"
                };
            }

            try
            {
                Random random = new Random();
                var code = random.Next(0000, 9999);

                string Message = @$"Hair Bello هذا هو رقم التفعيل الخاص ب تطبيق 
                    {code}
                    يمكنك اٍستخدام هذا الكود أكثر من مرة - لا تشاركه مع أحد
                    ";

                var model =  await _whisappService.Send("+972-" + result, Message);
                if(!model.IsSucceeded)
                {
                    return model;
                }
                var user = await _userRepository.GetUserByPhonNumber(PhonNumber);
               

                if (user.IsSucceeded)
                {
                    user.Value.Code = code.ToString();
                    var udated = await _userRepository.UpdateAsync(user.Value);
                    if (!udated.IsSucceeded)
                    {
                        return new ServiceReturnModel<bool>
                        {
                            IsSucceeded = false,
                            Comment = "حصل خطأ أثناء انشاء رمز"
                        };
                    }
                    return new ServiceReturnModel<bool>
                    {
                        IsSucceeded = true
                      ,
                        Comment = ""
                    };
                }
                else
                {
                    UserModel newmodel = new()
                    {
                        Code = code.ToString(),
                        PhoneNumber = PhonNumber,
                        IsBanned = false,
                        Name = username,
                        

                    };
                    var russ = await _userRepository.InsertAsync(newmodel);
                    if (!russ.IsSucceeded)
                    {
                        return new ServiceReturnModel<bool>
                        {
                            IsSucceeded = false
                       ,
                            Comment = "فشل انشاء مستخدم جديد"
                        };
                    }

                    return new ServiceReturnModel<bool>
                    {
                        IsSucceeded = true
                        ,
                        Comment = ""
                    };
                }
            }
            catch(Exception ex)
            {
                return new ServiceReturnModel<bool>
                {
                    IsSucceeded = false
                        ,
                    Comment = ex.Message
                };
            }
          
        

           
        }
    }
}
