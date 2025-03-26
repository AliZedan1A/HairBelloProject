

namespace Domain.DataTransfareObject
{
    public class UpdateBarberScheduleDto
    {
        public int Id { get; set; }
        public int BarberId { get; set; }
        public string DayName { get; set; }
        public TimeSpan StartHour { get; set; }
        public TimeSpan EndHour { get; set; }
    }
}
