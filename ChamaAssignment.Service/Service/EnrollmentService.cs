using ChamaAssignment.Data;
using ChamaAssignment.Service.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ChamaAssignment.Service.Service
{
    public class EnrollmentService : IEnrollmentService, IDisposable
    {
        private IDbContext dbContext;

        public EnrollmentService(IDbContext theDbContext)
        {
            dbContext = theDbContext;
        }

        /// <summary>
        /// Add a new enrollment entry to the database synchronously
        /// </summary>
        /// <param name="theCourseId">Course Id</param>
        /// <param name="theStudentId">Student Id</param>
        public bool AddEnrollment(string theCourseId, string theStudentId)
        {
            PrepareForEnrollment(theCourseId, theStudentId);

            try
            {
                int aResult = dbContext.SaveChanges();
                if(aResult > 0)
                {
                    
                    return true;
                }
                return false;
            }
            catch (DbUpdateConcurrencyException)
            {
                // Need to handle concurrency issue while signing up for a course
                return true;
            }
            catch(DbUpdateException)
            {
                //Log the exception message to a file
                throw; 
            }
        }

        /// <summary>
        /// Add a new enrollment entry to the database asynchronously
        /// </summary>
        /// <param name="theCourseId">Course Id</param>
        /// <param name="theStudentId">Student Id</param>
        public async Task<bool> AddEnrollmentAsync(string theCourseId, string theStudentId)
        {
            PrepareForEnrollment(theCourseId, theStudentId);

            try
            {
                int aResult =await dbContext.SaveChangesAsync();
                if (aResult > 0)
                {
                    return true;
                }
                return false;
            }
            catch (DbUpdateConcurrencyException)
            {
                // ToDO: Need to handle concurrency issue while signing up for a course
                return false;
            }
            catch (DbUpdateException)
            {
                //Log the exception message to a file
                throw;
            }
        }

        /// <summary>
        /// Check various conditions before commiting to database
        /// </summary>
        /// <param name="theCourseId">Course id</param>
        /// <param name="theStudentId">Student id</param>
        private void PrepareForEnrollment(string theCourseId, string theStudentId)
        {
            if (dbContext == null)
            {
                throw new Exception("Internal error has occurred");
            }
            if (!CourseExists(theCourseId))
            {
                //Log the error message 
                throw new KeyNotFoundException(string.Format("Course {0} does not exist", theCourseId));
            }
            if (!StudentExists(theStudentId))
            {
                //Log the error message 
                throw new KeyNotFoundException(string.Format("Student {0} does not exist", theStudentId));
            }

            Course aCurrentCourse = dbContext.Courses.FirstOrDefault(c => c.Id.Equals(theCourseId));
            if (aCurrentCourse.Size <= aCurrentCourse.Students.Count)
            {
                throw new Exception("Maximum count is reached for this course");
            }

            Student aExisting = dbContext.Courses.FirstOrDefault(c => c.Id == theCourseId).
                Students.FirstOrDefault(s => s.Id.Trim(' ').Equals(theStudentId));
            if (aExisting != null)
            {
                throw new Exception("You have already signed up for this course");
            }

            Course aCourse = new Course { Id = theCourseId };
            dbContext.Courses.Add(aCourse);
            dbContext.Courses.Attach(aCourse);

            Student aStudent = new Student { Id = theStudentId };
            dbContext.Students.Add(aStudent);
            dbContext.Students.Attach(aStudent);

            aCourse.Students.Add(aStudent);
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

        /// <summary>
        /// Check if the given student exists in the database
        /// </summary>
        /// <param name="id">Student Id</param>
        /// <returns>Boolean result</returns>
        private bool StudentExists(string id)
        {
            return dbContext.Students.Count(e => e.Id == id) > 0;
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