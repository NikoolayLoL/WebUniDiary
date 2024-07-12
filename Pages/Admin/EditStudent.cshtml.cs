using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUniDiary.Logic;
using WebUniDiary.Models;
using WebUniDiary.Models.DTOs;
using WebUniDiary.Services;

namespace WebUniDiary.Pages.Admin
{
    public class EditStudentModel : PageModel
    {
        public string failureMessage = string.Empty;
        [BindProperty]
        public User User { get; set; } = new User();
        [BindProperty]
        public Student Student { get; set; } = new Student();
        [BindProperty]
        public UserDto UserDto { get; set; } = new UserDto();
        [BindProperty]
        public StudentDto StudentDto { get; set; } = new StudentDto();
        private readonly ApplicationDbContext context;

        public EditStudentModel(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void OnGet(int userID = 0)
        {
            if (userID == 0)
            {
                Response.Redirect(CustomRedirect.RoleRedirect("admin") + "?success=&failure=invalid");
                return;
            }

            User = context.Users.Find(userID);
            if (User == null)
            {
                //TODO add a failure global URL and success. Pass keyIDs in order to show the proper failure message.
                Response.Redirect(CustomRedirect.RoleRedirect("admin") + "?success=&failure=invalid");
                return;
            }

            if (User.Role.ToLower() != "student")
            {
                //TODO add a failure global URL and success. Pass keyIDs in order to show the proper failure message.
                Response.Redirect(CustomRedirect.RoleRedirect("admin") + "?success=&failure=invalid");
                return;
            }

            Student = context.GetStudentByUserId(User.Id);
            if (Student == null)
            {
                Student = new Student() {
                    UserId = userID,
                    FacultyNumber = DateTime.Now.ToString("yyMMddHHmmss"),
                    Active = false,
                };

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex) { }
            }
        }

        public void OnPost(int userID = 0)
        {
            if (userID == 0)
            {
                failureMessage = "Incorrect User";
                Response.Redirect(CustomRedirect.RoleRedirect("admin") + "?success=&failure=invalid" + userID);
                return;
            }

            if (this.UserDto.Email == null)
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

            User = context.Users.Find(userID);
            if (User == null)
            {
                //TODO add a failure global URL and success. Pass keyIDs in order to show the proper failure message.
                Response.Redirect(CustomRedirect.RoleRedirect("admin") + "?success=&failure=invalid");
                return;
            }

            Student = context.GetStudentByUserId(User.Id);
            if (Student == null)
            {
                Student = new Student()
                {
                    UserId = User.Id,
                    FacultyNumber = DateTime.Now.ToString("yyMMddHHmmss"),
                    Active = false,
                };
            }

            User.Email = UserDto.Email;
            Student.Address = StudentDto.Address;
            Student.FirstName = StudentDto.FirstName;
            Student.LastName = StudentDto.LastName;
            Student.Active = StudentDto.Active;
            Student.PhoneNumber = StudentDto.PhoneNumber;
            Student.Specialty = StudentDto.Specialty;
            Student.EGN = StudentDto.EGN;

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                failureMessage = ex.Message;
                return;
            }

            Response.Redirect(CustomRedirect.RoleRedirect("admin") + "?success=editStudSucc&failure=");
        }
    }
}
