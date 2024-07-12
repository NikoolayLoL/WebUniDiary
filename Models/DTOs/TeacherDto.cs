using System.ComponentModel.DataAnnotations;

namespace WebUniDiary.Models.DTOs
{
    public class TeacherDto
    {
        [Required, MaxLength(30)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(30)]
        public string LastName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        [Required, MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string Address { get; set; } = string.Empty;
    }
}
