using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUniDiary.Logic;
using WebUniDiary.Models;
using WebUniDiary.Models.DTOs;
using WebUniDiary.Services;

namespace WebUniDiary.Pages.Admin
{
    public class EditTeacherModel : PageModel
    {
        public string failureMessage = string.Empty;
        [BindProperty]
        public UserDto UserDto { get; set; } = new UserDto();
        [BindProperty]
        public TeacherDto TeacherDto { get; set; } = new TeacherDto();
        public User User { get; set; } = new User();
        public Models.Teacher Teacher { get; set; } = new();

        private readonly ApplicationDbContext context;

        public EditTeacherModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void OnGet(int Id = 0)
        {
            if (Id == 0)
            {
                Response.Redirect(CustomRedirect.RoleRedirect("admin") + "?failure=invalid");
                return;
            }

            User = context.Users.Find(Id)!;

            if (User == null)
            {
                Response.Redirect(CustomRedirect.RoleRedirect("admin") + "?failure=invalid");
                return;
            }

            if (User.Role.ToLower() != "teacher")
            {
                //TODO add a failure global URL and success. Pass keyIDs in order to show the proper failure message.
                Response.Redirect(CustomRedirect.RoleRedirect("admin") + "?failure=invalid");
                return;
            }

            Teacher = context.GetTeacherByUserId(User.Id);
            if (Teacher == null)
            {
                Teacher = new Models.Teacher()
                {
                    UserId = User.Id,
                };

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex) { }
            }
        }

        public void OnPost(int Id = 0)
        {
            if (Id == 0)
            {
                Response.Redirect(CustomRedirect.RoleRedirect("admin") + "?failure=invalidID");
                return;
            }

            try
            {
                User = context.Users.Find(Id);
                Teacher = context.GetTeacherByUserId(User.Id);
            }
            catch (Exception ex)
            {
                Response.Redirect(CustomRedirect.RoleRedirect("admin") + "?failure=invalid");
                return;
            }

            if (User.Role.ToLower() != "teacher")
            {
                //TODO add a failure global URL and success. Pass keyIDs in order to show the proper failure message.
                Response.Redirect(CustomRedirect.RoleRedirect("admin") + "?failure=invalid");
                return;
            }

            User.Email = UserDto.Email;
            Teacher.Address = TeacherDto.Address;
            Teacher.PhoneNumber = TeacherDto.PhoneNumber;
            Teacher.FirstName = TeacherDto.FirstName;
            Teacher.LastName = TeacherDto.LastName;
            Teacher.Title = TeacherDto.Title;

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                failureMessage = ex.Message;
                return;
            }

            Response.Redirect(CustomRedirect.RoleRedirect("admin") + "?success=editTeacherSucc");
        }
    }
}
