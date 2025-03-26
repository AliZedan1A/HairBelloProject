using Domain.DatabaseModels;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using ServerSide.Database;
using System;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ServerSide.Repositories.Class
{
    public class PreviewImageRepository : Repository<PreviewImageModel>
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PreviewImageRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ServiceReturnModel<bool>> DeleteVideo(int ImageId)
        {
            // db image 
            var Image = await GetAsync(ImageId);
            if(!Image.IsSucceeded)
            {
                return new()
                {
                    IsSucceeded = false,
                    Comment = "لم يتم لتعرف على الفيديو في قواعد البيانات"
                };
            }
            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}s://{request.Host}/";

            var model = Directory.GetFiles("wwwroot/Uploads/Videos");
            foreach (var file in model)
            {
                if (file.Contains(Image.Value.Base64Url.Replace(baseUrl, "")))
                {
                    File.Delete(file);
                    var deletereq = await DeleteAsync(ImageId);
                    if(deletereq.IsSucceeded)
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
                            Comment = deletereq.Comment

                        };
                    }
                }
            }
            return new()
            {
                IsSucceeded = false,
                Comment = "لم يتم التعرف على الفيديو في الملفات"

            };
        }
        public async Task<ServiceReturnModel<bool>> DeleteImage(int ImageId)
        {
            var Image = await GetAsync(ImageId);
            if (!Image.IsSucceeded)
            {
                return new()
                {
                    IsSucceeded = false,
                    Comment = "لم يتم لتعرف على الفيديو في قواعد البيانات"
                };
            }
            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}s://{request.Host}/";

            var model = Directory.GetFiles("wwwroot/Uploads/Images");
            foreach (var file in model)
            {
                string newurl = Image.Value.Base64Url.Replace(baseUrl, "wwwroot/");
                string normalizedNewUrl = newurl.Replace('\\', '/');
                string normalizedFile = file.Replace('\\', '/');

               // bool f = file.Contains(newurl);
                if (normalizedFile.Contains(normalizedNewUrl))
                {
                    File.Delete(file);
                    var deletereq = await DeleteAsync(ImageId);
                    if (deletereq.IsSucceeded)
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
                            Comment = deletereq.Comment

                        };
                    }
                }
            }
            return new()
            {
                IsSucceeded = false,
                Comment = "لم يتم التعرف على الصورة في الملفات"

            };
        }
        public async Task<ServiceReturnModel<List<PreviewImageModel>>> GetImages()
        {
          var model = await  _context.Images.Where(x => x.IsVideo == false).ToListAsync();
            if (model.Count ==0)
            {
                return new ServiceReturnModel<List<PreviewImageModel>>()
                {
                    Comment = "لا يوجد صور",
                    IsSucceeded = false
                };
            }
            return new ServiceReturnModel<List<PreviewImageModel>>()
            {
                Value = model,
                IsSucceeded = true,

            };
        }
        public async Task<ServiceReturnModel<bool>> UpdateImage(int id , IFormFile file)
        {
          var delete  =   await DeleteImage(id);
            if (!delete.IsSucceeded)
            {
                return new ServiceReturnModel<bool>()
                {
                    IsSucceeded = false,
                    Comment = "فشل حذف الصورة السابقة عند التحديث"

                };
            }
            var add = await AddImage(file);
            if (!add.IsSucceeded)
            {
                return new ServiceReturnModel<bool>()
                {
                    IsSucceeded = false,
                    Comment = "فشل اضافة الصورة لجديدة عند التعديل"

                };
            }
            return new ServiceReturnModel<bool>()
            {
                IsSucceeded = true,

            };
        }
        public async Task<ServiceReturnModel<List<PreviewImageModel>>> GetVideos()
        {
          var model = await  _context.Images.Where(x => x.IsVideo == true).ToListAsync();
            if (model.Count == 0)
            {
                return new ServiceReturnModel<List<PreviewImageModel>>() {  IsSucceeded = false , Comment = "لا يوجد فيديوهات متاحة" };

            }
            return new ServiceReturnModel<List<PreviewImageModel>>() { Value = model, IsSucceeded =true};
        }
        public async Task<ServiceReturnModel<bool>> AddImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return new ServiceReturnModel<bool>
                {
                    Comment = "No file uploaded.",
                    IsSucceeded = false
                };

            // استخدام Guid لتوليد اسم فريد آمن للملف
            string randomString = Guid.NewGuid().ToString();
            string safeFileName = randomString + "_" + Path.GetFileName(file.FileName);

            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}s://{request.Host}/";

            // التأكد من وجود المجلد، وإنشاؤه إذا لم يكن موجوداً
            var folderPath = Path.Combine("wwwroot", "Uploads", "Images");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var path = Path.Combine(folderPath, safeFileName);

            // حفظ الملف على السيرفر
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // تحويل المسار ليصبح رابط URL صالح عن طريق استبدال wwwroot بالـ baseUrl
            var fileUrl = path.Replace("wwwroot\\", baseUrl).Replace("wwwroot/", baseUrl);

            var x = await InsertAsync(new PreviewImageModel { Base64Url = fileUrl, IsVideo = false });
            if (!x.IsSucceeded)
            {
                return new ServiceReturnModel<bool>
                {
                    Comment = "فشل اضافة الصورة.",
                    IsSucceeded = false
                };
            }
            return new ServiceReturnModel<bool>
            {
                IsSucceeded = true,
            };
        }
        public async Task<ServiceReturnModel<bool>> AddVideo(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return new ServiceReturnModel<bool>() { Comment = "No file uploaded.",
                IsSucceeded =false};
            var randomBytes = new byte[20];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            string str = Convert.ToBase64String(randomBytes);
            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}s://{request.Host}/";


            var path = Path.Combine("wwwroot/Uploads/Videos", str+file.FileName);
           
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            path = path.Replace("wwwroot/", baseUrl);

            var x = await InsertAsync(new PreviewImageModel() { Base64Url = path ,IsVideo=true});
            if (!x.IsSucceeded)
            {
                return new ServiceReturnModel<bool>()
                {   
                    Comment = "فشل اضافة الصورة.",
                    IsSucceeded = false
                };
            }
            return new ServiceReturnModel<bool>()
            {
                IsSucceeded = true,
            };
        }
    }
}
