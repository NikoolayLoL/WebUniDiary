namespace WebUniDiary.Models
{
    public class SemesterCourse
    {
        public int Id { get; set; }
        public int SemesterId { get; set; }
        public int CourseId { get; set; }
        // Comes from the Teacher that is assigned to the Course
        public int TeacherId { get; set; }
        // Use the UserId always
        public int SubTeacherId { get; set; }
    }
}
