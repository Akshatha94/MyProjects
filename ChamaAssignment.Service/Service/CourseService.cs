using AppInterface;
using ChamaAssignment.Data;
using ChamaAssignment.Service.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChamaAssignment.Service.Service
{
    public class CourseService : ICourseService
    {
        private IDbContext dbContext;

        public CourseService(IDbContext theDbContext)
        {
            dbContext = theDbContext;
        }

        /// <summary>
        ///  Returns a list with min, max and avg student age,
        ///  plus the course total capacity and current number of students
        /// </summary>
        /// <returns>List of StudentAgeReport data</returns>
        public IList<StudentAgeReport> GetStudentAgeReport()
        {
            IList<StudentAgeReport> aQueriedCourses = new List<StudentAgeReport>();
            if(dbContext == null)
            {
                //Log the error
                return null;
            }
            foreach (var dbCourse in dbContext.Courses)
            {
                if (dbCourse.Students.Count == 0)
                {
                    //No students have been signed up for this course
                    continue;
                }
                    
                StudentAgeReport aCourse = new StudentAgeReport();

                //Get the student age information and other details from DbContext class.
                aCourse.Id = dbCourse.Id;
                aCourse.MaxAge = dbCourse.Students.Max(st => st.Age);
                aCourse.MinAge = dbCourse.Students.Min(st => st.Age);
                aCourse.AvgAge = dbCourse.Students.Average(st => st.Age);
                aCourse.Capacity = dbCourse.Size;
                aCourse.Current = dbCourse.Students.Count;
                aQueriedCourses.Add(aCourse);
            }
            return aQueriedCourses;
        }

        /// <summary>
        /// Returns GetStudentAgeReport() data for a single course, plus the teacher and the list of registered students
        /// </summary>
        /// <param name="theCourseId">Course Id</param>
        /// <returns>Corresponding course information</returns>
        public CourseInfo GetCourseInfo(string theCourseId)
        {
            if (dbContext == null)
            {
                //Log the error
                return null;
            }
            CourseInfo aCourseInfo = new CourseInfo();

            //Find the course in the DB
            Course aCourse = dbContext.Courses.Find(theCourseId);
            if(!CourseExists(theCourseId))
            {
                //Log the error
                return null;
            }

            //Get the details from DbContext class.
            aCourseInfo.Id = aCourse.Id;
            aCourseInfo.MaxAge = aCourse.Students.Max(st => st.Age);
            aCourseInfo.MinAge = aCourse.Students.Min(st => st.Age);
            aCourseInfo.AvgAge = aCourse.Students.Average(st => st.Age);
            aCourseInfo.Teacher = aCourse.Lecturer;
            aCourseInfo.Current = aCourse.Students.Count;
            aCourseInfo.StudentCollection = aCourse.Students.ToList();
            return aCourseInfo;
        }

        /// <summary>
        /// Checks if the given course exists in the database
        /// </summary>
        /// <param name="id">Course id</param>
        /// <returns>Boolean result</returns>
        private bool CourseExists(string id)
        {
            return dbContext.Courses.Count(e => e.Id == id) > 0;
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dbContext != null)
                {
                    dbContext.Dispose();
                    dbContext = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}