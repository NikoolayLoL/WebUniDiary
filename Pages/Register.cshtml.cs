using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUniDiary.Logic;
using WebUniDiary.Models;
using WebUniDiary.Models.DTOs;
using WebUniDiary.Services;

namespace WebUniDiary.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public UserDto UserDto { get; set; } = new UserDto();
        private readonly ApplicationDbContext context;
        public string failureMessage = string.Empty;

        public RegisterModel(ApplicationDbContext context)
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

            User user = new User()
            {
                Email = UserDto.Email,
                Password = UserDto.Password,
                Role = UserDto.Role,
                CreatedAt = DateTime.Now,
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

            Cookie cookie = new Cookie(user.Id);
            HttpContext.Session.SetString(cookie.GetCookieId(), user.Role);
            HttpContext.Response.Cookies.Append("LoggedInUser", cookie.GetCookieId(), new CookieOptions
            {
                Expires = DateTime.Now.AddHours(1)
            });

            Response.Redirect(CustomRedirect.RoleRedirect(user.Role));
        }
    }
}
