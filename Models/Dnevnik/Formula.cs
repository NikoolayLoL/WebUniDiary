using System.ComponentModel.DataAnnotations.Schema;

namespace WebUniDiary.Models
{
    public class Formula
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        // Comes from the Course, but to make sure
        public int TeacherId { get; set; }
        public double MultiplierExam { get; set; }
        public double MultiplierWork { get; set; }
        public double MultiplierTask { get; set; }
        public double MultiplierAttention { get; set; }
        public double MultiplierExercises { get; set; }
        public double MultiplierExtra { get; set; }
        // Move in a Service
        public bool CanRecalculateGrade() => MultiplierExam != 0 || MultiplierWork != 0
            || MultiplierTask != 0 || MultiplierAttention != 0 || MultiplierExercises != 0
            || MultiplierExtra != 0;

    }
}
