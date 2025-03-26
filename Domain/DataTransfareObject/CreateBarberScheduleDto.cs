

namespace Domain.DataTransfareObject
{
    public class CreateBarberScheduleDto
    {
        public int BarberId { get; set; }
        public string DayName { get; set; }
        public TimeSpan StartHour { get; set; }
        public TimeSpan EndHour { get; set; }
    }
}
