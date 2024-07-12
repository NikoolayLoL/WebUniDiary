using System;
using System.Collections.Generic;
using System.Linq;
using WebUniDiary.Models;
using Microsoft.EntityFrameworkCore;
using WebUniDiary.Services;

namespace WebUniDiary.Services
{
    public class SemesterCourseService
    {
        private readonly ApplicationDbContext _context;

        public SemesterCourseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddSemesterCourse(int semesterId, int courseId, int teacherId, int subTeacherId)
        {
            var semesterCourse = new SemesterCourse
            {
                SemesterId = semesterId,
                CourseId = courseId,
                TeacherId = teacherId,
                SubTeacherId = subTeacherId
            };

            _context.SemesterCourses.Add(semesterCourse);
            _context.SaveChanges();
        }

        public List<SemesterCourse> GetAllSemesterCourses()
        {
            return _context.SemesterCourses.ToList();
        }

        public List<SemesterCourse> GetSemesterCoursesBySemesterId(int semesterId)
        {
            return _context.SemesterCourses.Where(sc => sc.SemesterId == semesterId).ToList();
        }

        public SemesterCourse GetSemesterCourseById(int id)
        {
            return _context.SemesterCourses.FirstOrDefault(sc => sc.Id == id);
        }

        public void UpdateSemesterCourse(SemesterCourse semesterCourse)
        {
            var existingSemesterCourse = _context.SemesterCourses.FirstOrDefault(sc => sc.Id == semesterCourse.Id);
            if (existingSemesterCourse != null)
            {
                existingSemesterCourse.SemesterId = semesterCourse.SemesterId;
                existingSemesterCourse.CourseId = semesterCourse.CourseId;
                existingSemesterCourse.TeacherId = semesterCourse.TeacherId;
                existingSemesterCourse.SubTeacherId = semesterCourse.SubTeacherId;

                _context.SaveChanges();
            }
        }

        //// Delete a SemesterCourse
        //public void DeleteSemesterCourse(int id)
        //{
        //    var semesterCourse = _context.SemesterCourses.FirstOrDefault(sc => sc.Id == id);
        //    if (semesterCourse != null)
        //    {
        //        _context.SemesterCourses.Remove(semesterCourse);
        //        _context.SaveChanges();
        //    }
        //}
    }
}

// USAGE IN CONTROLER!!!

//private readonly SemesterCourseService _semesterCourseService;

//public SemesterCourseController(SemesterCourseService semesterCourseService)
//{
//    _semesterCourseService = semesterCourseService;
//}