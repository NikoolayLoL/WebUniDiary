namespace WebUniDiary.Models
{
    public class Semester
    {
        public int Id { get; set; }
        public int SpecialtyBookId { get; set; }
        // Needs to be auto incremented if SpecialtyBookId is the same, do it in a Service
        public int SemesterNumber { get; set; }
    }
}
