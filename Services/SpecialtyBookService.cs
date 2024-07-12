using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using WebUniDiary.Models;

namespace WebUniDiary.Services
{
    public class SpecialtyBookService
    {
        private readonly ApplicationDbContext _context;

        public SpecialtyBookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddNewSpecialty(string Name, string Description, int semesterCount = 8)
        {
            var newSpecialty = new SpecialtyBook
            {
                Name = Name,
                Description = Description,
                CreatedOn = DateTime.Now,
                Active = true,
            };
            _context.SpecialtyBooks.Add(newSpecialty);
            _context.SaveChanges();


            var semesters = new List<Semester>();
            for (int i = 1; i <= semesterCount; i++)
            {
                semesters.Add(new Semester
                {
                    SemesterNumber = i,
                    SpecialtyBookId = newSpecialty.Id
                });
            }

            _context.Semesters.AddRange(semesters);
            _context.SaveChanges();
        }

        public List<SpecialtyBook> GetAllSpecialties()
        {
            return _context.SpecialtyBooks.ToList();
        }

        public DbSet<SpecialtyBook> GetAllSpecialtiesDbSet()
        {
            return _context.SpecialtyBooks;
        }

        public List<Semester> GetAllSpecialtySemesters(int specialtyBookId)
        {
            return _context.Semesters.Where(sem => sem.SpecialtyBookId == specialtyBookId).ToList();
        }

        public SpecialtyBook FindById(int id)
        {
            return _context.SpecialtyBooks.Find(id)!;
        }

        public void UpdateSpecialty(int id, string name, string description, bool active)
        {
            var specialty = _context.SpecialtyBooks.FirstOrDefault(sb => sb.Id == id);
            if (specialty != null)
            {
                specialty.Name = name;
                specialty.Description = description;
                specialty.Active = active;
                _context.SaveChanges();
            }
        }
    }
}
