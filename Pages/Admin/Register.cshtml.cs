using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUniDiary.Logic;
using WebUniDiary.Models.DTOs;
using WebUniDiary.Services;
using AdminUser = WebUniDiary.Models.Admin;

namespace WebUniDiary.Pages.Admin
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext context;

        [BindProperty]
        public AdminDto adminDto { get; set; } = new AdminDto();

        public RegisterModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void OnGet()
        {
            HttpContext.Request.Cookies.TryGetValue("LoggedInUser", out var cookieValue);
            Cookie cookie = new Cookie(cookieValue);

            AdminUser user = context.GetAdminByUserId(cookie.GetUserId());

            if (user != null)
            {
                Response.Redirect(CustomRedirect.RoleRedirect("admin"));
                return;
            }
        }

        public void OnPost()
        {
            AdminUser user = new AdminUser();

            HttpContext.Request.Cookies.TryGetValue("LoggedInUser", out var cookieValue);
            Cookie cookie = new Cookie(cookieValue);

            user.Preferences = adminDto.Preferences ?? "";
            user.UserId = cookie.GetUserId();

            context.Admins.Add(user);
            context.SaveChanges();

            Response.Redirect(CustomRedirect.RoleRedirect("admin"));
        }
    }
}
