using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUniDiary.Logic;
using WebUniDiary.Models;
using WebUniDiary.Models.DTOs;
using WebUniDiary.Services;

namespace WebUniDiary.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext context;

        [BindProperty]
        public UserDto userDto { get; set; } = new UserDto();

        public IndexModel(ApplicationDbContext context, ILogger<IndexModel> logger)
        {
            this.context = context;
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            var result = context.Users.FirstOrDefault(u => u.Email == userDto.Email && u.Password == userDto.Password);

            if (result == null)
            {
                return;
            }

            User user = result;
            Cookie cookie = new Cookie(user.Id);

            // If log in successful
            HttpContext.Session.SetString(cookie.GetCookieId(), user.Role);
            HttpContext.Response.Cookies.Append("LoggedInUser", cookie.GetCookieId(), new CookieOptions
            {
                Expires = DateTime.Now.AddHours(1)
            });

            Response.Redirect(CustomRedirect.RoleRedirect(user.Role));
        }

    }
}
