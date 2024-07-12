using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUniDiary.Models;
using WebUniDiary.Services;

namespace WebUniDiary.Pages.Admin
{
    public class BrowseSpecialtyBookModel : PageModel
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }

        public List<SpecialtyBook> specialtyBook { get; set; } = new List<SpecialtyBook>();

        private readonly SpecialtyBookService _specialtyBookService;
        private readonly ApplicationDbContext context;

        public BrowseSpecialtyBookModel(ApplicationDbContext context, SpecialtyBookService specialtyBookService)
        {
            this.context = context;
            this._specialtyBookService = specialtyBookService;
        }

        public void OnGet(int pageSize = 10, int currentPage = 1)
        {
            int skip = (currentPage - 1) * pageSize;

            var query = _specialtyBookService.GetAllSpecialtiesDbSet();

            // Search logic

            // Finalize search

            specialtyBook = query.OrderBy(x => x.Id).Skip(skip).Take(pageSize).ToList();

            int totalRecords = context.SpecialtyBooks.Count();
            int totalPages = (int) Math.Ceiling((double) totalRecords / pageSize);

            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            TotalRecords = totalRecords;
        }
    }
}
