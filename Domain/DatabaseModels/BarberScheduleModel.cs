using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.DatabaseModels
{
    public class BarberScheduleModel
    {
        [Key]
        public int Id { get; set; }
        public int BarberId { get; set; }
        [MaxLength(30, ErrorMessage = "يجب ألا يتجاوز اسم اليوم 30 حرفاً.")]
        public string DayName { get; set; }
        public TimeSpan StartHour { get; set; }
        public TimeSpan EndHour { get; set; }
        [JsonIgnore]
        public BarberModel Barber { get; set; }

    }
}