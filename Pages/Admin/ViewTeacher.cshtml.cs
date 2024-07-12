using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUniDiary.Models;
using WebUniDiary.Services;

namespace WebUniDiary.Pages.Admin
{
    public class ViewTeacherModel : PageModel
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public List<User> Users { get; set; } = new List<User>();
        public List<Models.Teacher> Teachers { get; set; } = new List<Models.Teacher>();

        private readonly ApplicationDbContext context;

        public ViewTeacherModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void OnGet(int pageSize = 10, int currentPage = 1, string Email = "", string FirstName = "", string LastName = "")
        {
            int skip = (currentPage - 1) * pageSize;

            var query = context.Users;

            // Search logic



            // Finalize search

            Users = query.Where(x => x.Role == "Teacher").OrderBy(x => x.Id).Skip(skip).Take(pageSize).ToList();
            Teachers = context.Teachers.ToList();

            int totalRecords = Users.Count();
            int totalPages = (int) Math.Ceiling((double) totalRecords / pageSize);

            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            TotalRecords = totalRecords;
        }
    }
}
