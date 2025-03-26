

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.DatabaseModels
{
    public class OrderModel
    {
        [Key]
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public UserModel User { get; set; }
        public int BarberId { get; set; }
        public bool IsCansled { get; set; }

        [JsonIgnore]
        public BarberModel Barber { get; set; }
        public List<OrderServiceModel> OrderServices { get; set; } = new();
    }
   
}
