
namespace Domain.DataTransfareObject
{
    public class CreateBarberBreak
    {
        public int BarberId { get; set; }
        public DateTime StartBreak { get; set; }
        public DateTime EndBreak { get; set; }

    }
    public class RemoveBarberBreak
    {
        public int BarberId { get; set; }

    }
}
