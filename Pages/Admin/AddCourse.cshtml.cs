using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUniDiary.Logic;
using WebUniDiary.Models;
using WebUniDiary.Models.DTOs;
using WebUniDiary.Services;

namespace WebUniDiary.Pages.Admin
{
    public class AddCourseModel : PageModel
    {
        public string failureMessage = string.Empty;

        [BindProperty]
        public CourseDto CourseDto { get; set; } = new CourseDto();

        private readonly ApplicationDbContext context;
        public AddCourseModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void OnGet(string message)
        {
            //failureMessage = message ?? string.Empty;
        }

        public void OnPost()
        {
            if (this.CourseDto.Name == string.Empty || this.CourseDto.Description == string.Empty)
            {
                failureMessage = "RequiredEmpty";
                return;
            }

            if (CourseDto.TeacherID != 0)
            {
                var user = context.Teachers.Find(CourseDto.TeacherID);

                if (user == null)
                {
                    failureMessage = "No Such Teacher!";
                    return;
                }
            }

            Course course = new Course
            {
                Name = CourseDto.Name,
                Description = CourseDto.Description,
                TeacherID = CourseDto.TeacherID,
            };

            try
            {
                context.Courses.Add(course);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                failureMessage = "Unable to save record";
                return;
            }

            Response.Redirect(CustomRedirect.RoleRedirect("admin") + "/?Success=CourseAdded");
            return;
        }
    }
}
