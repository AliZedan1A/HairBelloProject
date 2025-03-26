

namespace Domain.DataTransfareObject
{
    public class CreateOrderDto
    {
        public double TotalPrice { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public string PhonNumber { get; set; }
        public int BarberId { get; set; }
        public DateTime Date { get; set; }
        public List<CreateOrderServiceDto> Services { get; set; }
    }

}
