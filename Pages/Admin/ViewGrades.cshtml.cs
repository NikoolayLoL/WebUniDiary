using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUniDiary.Logic;
using WebUniDiary.Models;
using WebUniDiary.Services;

namespace WebUniDiary.Pages.Admin
{
    public class ViewGradesModel : PageModel
    {
        private readonly SpecialtyBookService _specialtyBookService;
        private readonly StudentBookService _studentBookService;
        private readonly SemesterCourseService _semesterCourseService;
        private readonly ApplicationDbContext _context;

        public SpecialtyBook SpecialtyBook { get; set; }
        public List<Course> Courses { get; set; }

        public ViewGradesModel(SpecialtyBookService specialtyBookService, StudentBookService studentBookService, SemesterCourseService semesterCourseService, ApplicationDbContext context)
        {
            _specialtyBookService = specialtyBookService;
            _studentBookService = studentBookService;
            _semesterCourseService = semesterCourseService;
            _context = context;
        }

        public void OnGet(int id = 0)
        {
            if (0 == id)
            {
                Response.Redirect(CustomRedirect.RoleRedirect("admin") + "?failure=viewGrades");
                return;
            }

            SpecialtyBook = _specialtyBookService.FindById(id);
            
        }
    }
}
