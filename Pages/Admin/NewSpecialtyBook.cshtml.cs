using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebUniDiary.Logic;
using WebUniDiary.Models;
using WebUniDiary.Services;

namespace WebUniDiary.Pages.Admin
{
    public class NewSpecialtyBook : PageModel
    {
        private readonly SpecialtyBookService _specialtyBookService;
        private readonly ApplicationDbContext context;

        [BindProperty]
        public string Name { get; set; } = string.Empty;
        [BindProperty]
        public string Description { get; set; } = string.Empty;
        [BindProperty]
        public int SemesterCount { get; set; } = 8;

        public NewSpecialtyBook(SpecialtyBookService specialtyBookService, ApplicationDbContext context)
        {
            _specialtyBookService = specialtyBookService;
            this.context = context;
        }


        public void OnGet()
        {

        }

        public void OnPost()
        {
            if (Name == string.Empty)
            {
                return;
            }

            try
            {
                _specialtyBookService.AddNewSpecialty(Name, Description, SemesterCount);
            }
            catch (Exception ex)
            {
                return;
            }
            // TO DO ADD EACH CONTROLLER AS ACTION
            Response.Redirect(CustomRedirect.RoleRedirect("admin") + "/BrowseSpecialtyBook" + "/?success=addedSucc");
        }
    }
}
