using System.ComponentModel.DataAnnotations;

namespace WebUniDiary.Models
{
    public class Student
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FacultyNumber { get; set; } = string.Empty;
        [MaxLength(30)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(30)]
        public string LastName { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        [MaxLength(15)]
        public string EGN { get; set; } = string.Empty;
        [MaxLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool Active { get; set; } = false;
    }
}
