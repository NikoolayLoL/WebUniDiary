using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WebUniDiary.Models
{
    public class Grade
    {
        public int Id { get; set; }
        // To who it's written
        public int StudentBookId { get; set; }
        // Which Course and Semester
        public int SemesterCourseId { get; set; }
        // It can be the SubTeacher that wants to grade
        public int TeacherId { get; set; }

        public enum GradeType
        {
            GradeExercises = 1,
            GradeAttendence = 2,
            GradeTask = 3,
            GradeWork = 4,
            GradeExam = 5,
            GradeExtra = 6,
        };
        public double GradeValue { get; set; }
        public string Description { get; set; } = string.Empty;

    }
}
