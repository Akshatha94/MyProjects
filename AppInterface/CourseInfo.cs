using ChamaAssignment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInterface
{
    /// <summary>
    /// Holds teacher and current number of students information
    /// </summary>
    public class CourseInfo : StudentAgeReport
    {
        public string Teacher { get; set; }
        public List<Student> StudentCollection { get; set; }
    }
}
