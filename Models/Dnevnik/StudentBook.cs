namespace WebUniDiary.Models
{
    public class StudentBook
    {
        public int Id { get; set; }
        public int SpecialtyBookId { get; set; }
        public int StudentId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public DateTime GraduationDate { get; set; }
        // The idea is that a Student can exist, however until he is registed here
        // he is considered without a specialty. Aka until they have a record here it don't count
    }
}
