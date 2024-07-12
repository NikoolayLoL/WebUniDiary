namespace WebUniDiary.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Preferences { get; set; } = string.Empty;
    }
}
