using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using WebUniDiary.Models;

namespace WebUniDiary.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Formula> Formulas { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<SemesterCourse> SemesterCourses { get; set; }
        public DbSet<SpecialtyBook> SpecialtyBooks { get; set; }
        public DbSet<StudentBook> StudentBooks { get; set; }
        public DbSet<StudentSemesterCourseGrade> StudentSemesterCourseGrades { get; set; }

        public Admin GetAdminByUserId(int userId) => Admins.FirstOrDefault(s => s.UserId == userId)!;
        public Student GetStudentByUserId(int userId) => Students.FirstOrDefault(s => s.UserId == userId)!;
        public Teacher GetTeacherByUserId(int userId) => Teachers.FirstOrDefault(s => s.UserId == userId)!;
        public List<Course> GetCoursesByTeacherId(int teacherId) => Courses.Where(y => y.TeacherID == teacherId).ToList();
    }
}
