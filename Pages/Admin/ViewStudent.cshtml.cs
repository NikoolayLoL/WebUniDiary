using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebUniDiary.Models;
using WebUniDiary.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebUniDiary.Pages.Admin
{
    public class ViewStudentModel : PageModel
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public string ExtraField { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName{ get; set; }
        public int ExtraChoice = 0;

        public List<User> Users { get; set; } = new List<User>();
        public List<Student> Students { get; set; } = new List<Student>();

        private readonly ApplicationDbContext context;

        public ViewStudentModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void OnGet(int pageSize = 10, int currentPage = 1, string Email = "", string FirstName = "", string LastName = "")
        {
            int skip = (currentPage - 1) * pageSize;

            var query = context.Students;

            if (Email != string.Empty)
            {
                Users = context.Users.Where(x => x.Email == Email).ToList();
            }
            else
            {
                switch (ExtraChoice)
                {
                    // Faculty Number
                    case 1:
                        query.Where(x => x.FacultyNumber == ExtraField);
                        break;
                    // EGN
                    case 2:
                        query.Where(x => x.EGN == ExtraField);
                        break;
                    // Specialty
                    case 3:
                        query.Where(x => x.Specialty == ExtraField);
                        break;
                    default:
                        break;
                }

                if (FirstName != string.Empty)
                {
                    //query.Where(x => x.FirstName == FirstName);
                    query.FromSqlRaw("SELECT * FROM Students WHERE FirstName LIKE = \"%`" + FirstName + "`%\"");
                }

                if (LastName != string.Empty)
                {
                    query.Where(x => x.FirstName == LastName);
                }

                Students = query.ToList();
            }

            if (Users.Count == 0)
            {
                Users = context.Users.Where(x => x.Role == "Student").OrderBy(x => x.Id).Skip(skip).Take(pageSize).ToList();
            }

            if (Students.Count == 0)
            {
                Students = context.Students.ToList();
            }

            int totalRecords = context.Users.Where(x => x.Role == "Student").Count();
            int totalPages = (int) Math.Ceiling((double) totalRecords / pageSize);

            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            TotalRecords = totalRecords;
        }

        public void OnPost()
        {
        }
    }
}
