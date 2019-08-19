using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaAssignment.Service.Service.Interfaces
{
    public interface IEnrollmentService
    {
        bool AddEnrollment(string theCourseId, string theStudentId);
        Task<bool> AddEnrollmentAsync(string theCourseId, string theStudentId);
    }
}
