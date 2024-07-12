using System.ComponentModel.DataAnnotations;

namespace WebUniDiary.Models
{
    public class User
    {
        public int Id { get; set; }
        [MaxLength(40)]
        public string Email { get; set; } = string.Empty;
        [MaxLength(40)]
        public string Password { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Role { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
