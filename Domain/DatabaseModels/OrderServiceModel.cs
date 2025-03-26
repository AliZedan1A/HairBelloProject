
using System.Text.Json.Serialization;

namespace Domain.DatabaseModels
{
    public class OrderServiceModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        [JsonIgnore]
        public OrderModel Order { get; set; }
        public int ServiceId { get; set; }
        public ServiceModel Service { get; set; }

    }
}