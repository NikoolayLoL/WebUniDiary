using System.ComponentModel.DataAnnotations;

namespace WebUniDiary.Models
{
    public class SpecialtyBook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public bool Active { get; set; } = true;
    }
}
