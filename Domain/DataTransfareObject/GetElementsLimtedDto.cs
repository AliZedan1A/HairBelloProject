using System.ComponentModel.DataAnnotations;

namespace Domain.DataTransfareObject
{
    public class GetElementsLimtedDto
    {
        [Required]
        public int Start { get; set; }
        [Required]
        public int End { get; set; }
        [Required]
        public int BarberId { get; set; }
        [Required]
        public DateTime date { get; set; }
    }
    public class GetElementsLimtedSortedByDay
    {
        [Required]
        public int Start { get; set; }
        [Required]
        public int End { get; set; }
        [Required]
        public int BarberId { get; set; }
    }
}
