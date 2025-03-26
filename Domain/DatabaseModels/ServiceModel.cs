
using System.Text.Json.Serialization;

namespace Domain.DatabaseModels
{
    public class ServiceModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Time { get; set; } 
        public double Price { get; set; }

        public int BarberId { get; set; }
        [JsonIgnore]
        public BarberModel Barber { get; set; }

    }
}
