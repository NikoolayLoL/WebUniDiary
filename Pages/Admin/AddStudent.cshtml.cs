using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUniDiary.Logic;
using WebUniDiary.Models;
using WebUniDiary.Models.DTOs;
using WebUniDiary.Services;

namespace WebUniDiary.Pages.Admin
{
    public class AddStudentModel : PageModel
    {
        public string failureMessage = string.Empty;
        [BindProperty]
        public UserDto UserDto { get; set; } = new UserDto();
        [BindProperty]
        public StudentDto StudentDto { get; set; } = new StudentDto();
        private readonly ApplicationDbContext context;

        public AddStudentModel(ApplicationDbContext context)
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
            if (this.StudentDto.FirstName == null || this.StudentDto.LastName == null)
            { 
                failureMessage = "Incorrect name. Please try again.";
                return;
            }
            if (this.StudentDto.EGN == null || this.StudentDto.PhoneNumber == null)
            {
                failureMessage = "Incorrect digits. Please try again.";
                return;
            }
            if (this.StudentDto.Address == null)
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

            Student student = new Student()
            {
                FirstName = this.StudentDto.FirstName,
                LastName = this.StudentDto.LastName,
                Address = this.StudentDto.Address,
                EGN = this.StudentDto.EGN,
                PhoneNumber = this.StudentDto.PhoneNumber,
                FacultyNumber = DateTime.Now.ToString("yyMMddHHmmss"),
                Specialty = this.StudentDto.Specialty,
                Active = this.StudentDto.Active,
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
            student.UserId = user.Id;

            try
            {
                context.Students.Add(student);
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
