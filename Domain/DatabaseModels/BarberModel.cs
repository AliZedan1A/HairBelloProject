
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.DatabaseModels
{
    public class BarberModel
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100, ErrorMessage = "يجب ألا يتجاوز الاسم 100 حرف.")]
        public string Name { get; set; }
        public bool IsInBreak { get; set; }
        public DateTime StartBreak { get; set; }
        public DateTime EndBreak { get; set; }
        public List<ServiceModel> Services { get; set; } = new();
        public List<BarberScheduleModel> BarberSchedules { get; set; } = new();
        public List<OrderModel> Orders { get; set; } = new();
    }
}
