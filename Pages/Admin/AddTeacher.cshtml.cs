using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUniDiary.Logic;
using WebUniDiary.Models;
using WebUniDiary.Models.DTOs;
using WebUniDiary.Services;

namespace WebUniDiary.Pages.Admin
{
    public class AddTeacherModel : PageModel
    {
        public string failureMessage = string.Empty;
        [BindProperty]
        public UserDto UserDto { get; set; } = new UserDto();
        [BindProperty]
        public TeacherDto TeacherDto { get; set; } = new TeacherDto();
        private readonly ApplicationDbContext context;

        public AddTeacherModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (this.UserDto.Password == null || this.UserDto.Email == null)
            {
                failureMessage = "Incorrect credentials. Please try again.";
                return;
            }
            if (this.TeacherDto.FirstName == null || this.TeacherDto.LastName == null)
            {
                failureMessage = "Incorrect name. Please try again.";
                return;
            }
            if (this.TeacherDto.PhoneNumber == null)
            {
                failureMessage = "Incorrect digits. Please try again.";
                return;
            }
            if (this.TeacherDto.Address == null || this.TeacherDto.Title == null)
            {
                failureMessage = "Incorrect address. Please try again.";
                return;
            }

            User user = new User()
            {
                Email = UserDto.Email,
                Password = UserDto.Password,
                Role = UserDto.Role,
                CreatedAt = DateTime.Now,
            };

            Models.Teacher teacher = new Models.Teacher()
            {
                FirstName = this.TeacherDto.FirstName,
                LastName = this.TeacherDto.LastName,
                Address = this.TeacherDto.Address,
                PhoneNumber = this.TeacherDto.PhoneNumber,
                Title = this.TeacherDto.Title,
                UserId = context.Users.Count() + 1, // just for reference, is not correct though
            };

            try
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                failureMessage = ex.Message;
                return;
            }

            user = context.Users.FirstOrDefault(s => s.Email == UserDto.Email)!;
            teacher.UserId = user.Id;

            try
            {
                context.Teachers.Add(teacher);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                failureMessage = ex.Message;
                return;
            }

            Response.Redirect(CustomRedirect.RoleRedirect("admin") + "/?success=addedSucc");
        }
    }
}
