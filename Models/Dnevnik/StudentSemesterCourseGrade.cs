namespace WebUniDiary.Models
{
    public class StudentSemesterCourseGrade
    {
        public int Id { get; set; }
        // This handles the Subject and the Semester
        public int SemesterCourseId { get; set; }
        // For who the grade is
        public int StudentBookId { get; set; }

        // Final student grade for a given Semester Course, calculated via the Formula + all Grades in the table
        public double Grade { get; set; }

    }
}
