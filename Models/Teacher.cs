using System.ComponentModel.DataAnnotations;

namespace WebUniDiary.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [MaxLength(30)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(30)]
        public string LastName { get; set; } = string.Empty;
        [MaxLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}
