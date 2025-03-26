

namespace Domain.Models
{
    public class AvailableSlotModel
    {
        public string Start { get; set; } // وقت البداية بصيغة "HH:mm"
        public string End { get; set; }   // وقت النهاية بصيغة "HH:mm"
    }
}
