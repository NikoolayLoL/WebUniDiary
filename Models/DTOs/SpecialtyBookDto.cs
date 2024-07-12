using System.ComponentModel.DataAnnotations;

namespace WebUniDiary.Models.DTOs
{
    public class SpecialtyBookDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        [Required]
        public bool Active { get; set; } = true;
    }
}
