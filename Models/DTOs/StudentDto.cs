using System.ComponentModel.DataAnnotations;

namespace WebUniDiary.Models.DTOs
{
    public class StudentDto
    {
        [Required, MaxLength(30)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(30)]
        public string LastName { get; set; } = string.Empty;
        [Required, MaxLength(15)]
        public string EGN { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        [Required, MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string Address { get; set; } = string.Empty;
        public bool Active { get; set; } = false;
    }
}
