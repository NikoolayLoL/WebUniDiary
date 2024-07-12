using System.ComponentModel.DataAnnotations;

namespace WebUniDiary.Models.DTOs
{
    public class UserDto
    {
        [Required, MaxLength(40)]
        public string Email { get; set; } = string.Empty;
        [Required, MaxLength(40)]
        public string Password { get; set; } = string.Empty;
        [Required, MaxLength(40)]
        public string Role { get; set; } = string.Empty;
    }
}
