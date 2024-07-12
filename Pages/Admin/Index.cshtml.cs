using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
using WebUniDiary.Logic;
using WebUniDiary.Models;
using WebUniDiary.Services;
using Cookie = WebUniDiary.Logic.Cookie;
using AdminUser = WebUniDiary.Models.Admin;

namespace WebUniDiary.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public string isAccountSetUp = "true";
        public string successMessage = "false";
        public string failureMessage = "false";
        public int totalStudents { get; set; }
        public int totalTeachers { get; set; }
        public int totalCourses { get; set; }

        //private readonly UserContext _userContext;
        private readonly ApplicationDbContext context;

        public IndexModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void OnGet(string Success = "", string Failure = "")
        {
            HttpContext.Request.Cookies.TryGetValue("LoggedInUser", out var cookieValue);
            Cookie cookie = new Cookie(cookieValue);

            AdminUser user = context.GetAdminByUserId(cookie.GetUserId());

            if (user == null)
            {
                isAccountSetUp = "false";
            }

            this.successMessage = Success ?? "";
            this.failureMessage = Failure ?? "";

            totalStudents = context.Students.Count();
            totalTeachers = context.Teachers.Count();
            totalCourses = context.Courses.Count();
        }
    }
}
