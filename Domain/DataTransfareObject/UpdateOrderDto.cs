

namespace Domain.DataTransfareObject
{
    public class UpdateOrderDto
    {
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public int UserId { get; set; }
        public int BarberId { get; set; }
    }
}
