using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using WebUniDiary.Logic;
using WebUniDiary.Models;
using WebUniDiary.Models.DTOs;
using WebUniDiary.Services;

namespace WebUniDiary.Pages.Admin
{
    public class ModifySpecialtyBookModel : PageModel
    {
        [BindProperty]
        public SpecialtyBookDto specialtyBookDto { get; set; } = new ();
        public SpecialtyBook specialtyBook { get; set; } = new ();
        public List<Semester> semesters { get; set; } = new ();
        public List<SemesterCourse> semesterCourses { get; set; } = new ();
        public List<Course> courses { get; set; } = new ();

        private readonly SpecialtyBookService _specialtyBookService;
        private readonly SemesterCourseService _semesterCourseService;
        private readonly ApplicationDbContext _context;

        public ModifySpecialtyBookModel(ApplicationDbContext context, SpecialtyBookService specialtyBookService, SemesterCourseService semesterCourseService)
        {
            this._context = context;
            this._specialtyBookService = specialtyBookService;
            this._semesterCourseService = semesterCourseService;
        }

        public void OnGet(int id = 0)
        {
            if (0 == id)
            {
                Response.Redirect(CustomRedirect.RoleRedirect("admin") + "?failure=editSpecialtyBook");
            }

            specialtyBook = _specialtyBookService.FindById(id);

            if (null == specialtyBook)
            {
                //TODO add a failure global URL and success. Pass keyIDs in order to show the proper failure message.
                Response.Redirect(CustomRedirect.RoleRedirect("admin") + "?failure=editSpecialtyBook");
                return;
            }

            semesters = _specialtyBookService.GetAllSpecialtySemesters(specialtyBook.Id);
            // 8 semester unique ids, could be 10 or 12 or whatever amount
            try
            {
                foreach (var semester in semesters)
                {
                    var coursesForSemester = _semesterCourseService.GetSemesterCoursesBySemesterId(semester.Id);
                    semesterCourses.AddRange(coursesForSemester);
                }
            }
            catch(Exception ex)
            {
            }

            courses = _context.Courses.ToList();
        }

        public void OnPost(int id = 0)
        {
            if (0 == id)
            {
                Response.Redirect(CustomRedirect.RoleRedirect("admin") + "?failure=editSpecialtyBook");
            }

            specialtyBook = _specialtyBookService.FindById(id);

            try
            {
                _specialtyBookService.UpdateSpecialty(id, specialtyBookDto.Name, specialtyBookDto.Description, specialtyBookDto.Active);
            }
            catch (Exception ex)
            {
                // TO DO FAILURE MESSAGE>?
                return;
            }

            Response.Redirect(CustomRedirect.RoleRedirect("admin") + "/BrowseSpecialtyBook?success=modified");
        }

        public IActionResult OnGetModalData()
        {
            var courses = _context.Courses.ToList(); // Assume _context is your DbContext
            return new JsonResult(new { courses = courses });
        }

        public IActionResult OnGetSaveCourseSelection(string semesterId, string courseId)
        {
            // Validate the input
            if (string.IsNullOrEmpty(semesterId) || string.IsNullOrEmpty(courseId))
            {
                return BadRequest("Invalid data.");
            }

            if (!int.TryParse(semesterId, out int semId) || !int.TryParse(courseId, out int courId))
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                // Save to the database
                var semesterCourse = new SemesterCourse
                {
                    SemesterId = semId,
                    CourseId = courId
                };
                _context.SemesterCourses.Add(semesterCourse);
                _context.SaveChanges();

                // Return the course details to display
                var course = _context.Courses.Find(courId);
                var courseName = course?.Name;
                var courseDescription = course?.Description;

                return new JsonResult(new { courseName = courseName, courseDescription = courseDescription });
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
