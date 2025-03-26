

namespace Domain.DataTransfareObject
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public bool IsBanned { get; set; }
    }
}
