
using System.Text.Json.Serialization;

namespace Domain.DatabaseModels
{
    public class UserModel
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsBanned { get; set; }
        [JsonIgnore]
        public List<OrderModel> Orders { get; set; } = new();
    }
}
