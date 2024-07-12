using System;
using System.Collections.Generic;
using System.Linq;
using WebUniDiary.Models;
using Microsoft.EntityFrameworkCore;
using WebUniDiary.Services;

namespace WebUniDiary.Services
{
    public class StudentBookService
    {
        private readonly ApplicationDbContext _context;

        public StudentBookService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Add a new StudentBook
        public void AddStudentBook(int specialtyBookId, int studentId, DateTime enrollmentDate, DateTime graduationDate)
        {
            var studentBook = new StudentBook
            {
                SpecialtyBookId = specialtyBookId,
                StudentId = studentId,
                EnrollmentDate = enrollmentDate,
                GraduationDate = graduationDate
            };

            _context.StudentBooks.Add(studentBook);
            _context.SaveChanges();
        }

        // Get all StudentBooks
        public List<StudentBook> GetAllStudentBooks()
        {
            return _context.StudentBooks.ToList();
        }

        public List<StudentBook> GetAllStudents(int id)
        {
            return _context.StudentBooks.Where(book => book.SpecialtyBookId == id).ToList();
        }

        // Get StudentBooks by StudentId
        public List<StudentBook> GetStudentBooksByStudentId(int studentId)
        {
            return _context.StudentBooks.Where(sb => sb.StudentId == studentId).ToList();
        }

        // Get StudentBook by Id
        public StudentBook GetStudentBookById(int id)
        {
            return _context.StudentBooks.FirstOrDefault(sb => sb.Id == id);
        }

        // Update an existing StudentBook
        public void UpdateStudentBook(StudentBook studentBook)
        {
            var existingStudentBook = _context.StudentBooks.FirstOrDefault(sb => sb.Id == studentBook.Id);
            if (existingStudentBook != null)
            {
                existingStudentBook.SpecialtyBookId = studentBook.SpecialtyBookId;
                existingStudentBook.StudentId = studentBook.StudentId;
                existingStudentBook.EnrollmentDate = studentBook.EnrollmentDate;
                existingStudentBook.GraduationDate = studentBook.GraduationDate;

                _context.SaveChanges();
            }
        }

        // Delete a StudentBook
        //public void DeleteStudentBook(int id)
        //{
        //    var studentBook = _context.StudentBooks.FirstOrDefault(sb => sb.Id == id);
        //    if (studentBook != null)
        //    {
        //        _context.StudentBooks.Remove(studentBook);
        //        _context.SaveChanges();
        //    }
        //}
    }
}

//private readonly StudentBookService _studentBookService;

//public StudentBookController(StudentBookService studentBookService)
//{
//    _studentBookService = studentBookService;
//}
