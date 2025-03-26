

using System.ComponentModel.DataAnnotations;

namespace Domain.DatabaseModels
{
    public class AppConfigModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "الرجاء إدخال رقم هاتف الأدمن.")]
        [Phone(ErrorMessage = "صيغة رقم الهاتف غير صحيحة.")]
        [Display(Name = "رقم هاتف الأدمن")]
        public string AdminPhonNumber { get; set; }

        public bool IsHairBelloWork { get; set; }
    }
}
