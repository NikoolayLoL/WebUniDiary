using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUniDiary.Logic;
using WebUniDiary.Models;
using WebUniDiary.Services;

namespace WebUniDiary.Pages.Admin
{
    public class ViewStudentBookModel : PageModel
    {
        public List<StudentBook> studentBooks { get; set; } = new List<StudentBook>();
        public List<Student> students { get; set; } = new List<Student>();
        public SpecialtyBook specialty { get; set; } = new();
        public int specialtyId { get; set; }

        private ApplicationDbContext context;
        private SpecialtyBookService _specialtyBookService;
        private readonly StudentBookService studentBookService;

        public ViewStudentBookModel(ApplicationDbContext context, SpecialtyBookService specialtyBookService, StudentBookService studentBookService)
        {
            this.context = context;
            this._specialtyBookService = specialtyBookService;
            this.studentBookService = studentBookService;
        }

        public void OnGet(int Id = 0)
        {
            if (0 == Id)
            {
                Response.Redirect(CustomRedirect.RoleRedirect("admin") + "/?failure=NoSuchSpecialty");
                return;
            }

            specialtyId = Id;
            specialty = context.SpecialtyBooks.Find(Id);
            studentBooks = studentBookService.GetAllStudents(Id);

            if (studentBooks == null)
            {
                Response.Redirect(CustomRedirect.RoleRedirect("admin") + "/?failure=NoSuchSpecialty");
                return;
            }

            studentBooks.ForEach(studentBook =>
            {
                Student user = context.Students.FirstOrDefault(x => x.UserId == studentBook.StudentId)!;
                students.Add(user);
            });
        }

        public IActionResult OnGetAssignStudent(int Id,string email)
        {
            if (string.IsNullOrEmpty(email))
            {

                return BadRequest("Invalid email.");
            }

            var user = context.Users.FirstOrDefault(s => s.Email == email);
            if (user == null)
            {
                return NotFound("Student not found.");
            }

            int semestersCount = context.Semesters.Where(semester => semester.SpecialtyBookId == Id).Count();
            int years = (int) Math.Ceiling(semestersCount / 2.0);

            DateTime currentDateTime = DateTime.UtcNow;
            DateTime futureDateTime = currentDateTime.AddYears(years);

            studentBookService.AddStudentBook(Id, user.Id, currentDateTime, futureDateTime);

            return new JsonResult(new { success = true });
        }
    }
}
