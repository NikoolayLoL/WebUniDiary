using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUniDiary.Logic;
using WebUniDiary.Models;
using WebUniDiary.Models.DTOs;
using WebUniDiary.Services;

namespace WebUniDiary.Pages.Admin
{
    public class EditCourseModel : PageModel
    {
        public string failureMessage = string.Empty;

        [BindProperty]
        public CourseDto CourseDto { get; set; } = new CourseDto();
        [BindProperty]
        public Course Course { get; set; } = new Course();
        [BindProperty]
        public TeacherDto TeacherDto { get; set; } = new TeacherDto();
        [BindProperty]
        public Models.Teacher Teacher { get; set; } = new Models.Teacher();

        private readonly ApplicationDbContext context;
        public EditCourseModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void OnGet(int courseId = 0)
        {
            if (courseId == 0)
            {
                Response.Redirect(CustomRedirect.RoleRedirect("admin") + "?failure=invalid");
                return;
            }

            Course = context.Courses.Find(courseId)!;
            if (Course == null)
            {
                //TODO add a failure global URL and success. Pass keyIDs in order to show the proper failure message.
                Response.Redirect(CustomRedirect.RoleRedirect("admin") + "?success=&failure=invalid");
                return;
            }

            Teacher = context.Teachers.Find(Course.TeacherID)!;
            // Could be null
        }

        public void OnPost(int courseId = 0)
        {
            if (courseId == 0)
            {
                Response.Redirect(CustomRedirect.RoleRedirect("admin") + "?failure=invalid");
                return;
            }

            if (CourseDto.Name == null)
            {
                failureMessage = "Invalid course name.";
                return;
            }

            if (CourseDto.Description == null)
            {
                failureMessage = "Invalid course description.";
                return;
            }

            Course = context.Courses.Find(courseId)!;

            if (CourseDto.TeacherID != 0)
            {
                var user = context.Teachers.Find(CourseDto.TeacherID);

                if (user == null)
                {
                    failureMessage = "No Such Teacher!";
                    return;
                }

                Course.TeacherID = CourseDto.TeacherID;
            }

            Course.Name = CourseDto.Name;
            Course.Description = CourseDto.Description;

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                failureMessage = ex.Message;
                return;
            }

            Response.Redirect(CustomRedirect.RoleRedirect("admin") + "?success=editCourseSucc");
        }
    }
}
