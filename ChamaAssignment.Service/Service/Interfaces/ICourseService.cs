using AppInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaAssignment.Service.Service.Interfaces
{
    public interface ICourseService
    {
        IList<StudentAgeReport> GetStudentAgeReport();
        CourseInfo GetCourseInfo(string theCourseId);
    }
}
