using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInterface
{
    /// <summary>
    /// Holds minimun, maximum and average age of the students for a course plus course total capacity and
    /// current number of students
    /// </summary>
    public class StudentAgeReport
    {
        public string Id { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public double AvgAge { get; set; }
        public int Capacity { get; set; }
        public int Current { get; set; }
    }
}
