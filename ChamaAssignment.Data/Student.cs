//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChamaAssignment.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student
    {
        public Student()
        {
            this.Courses = new HashSet<Course>();
        }
    
        public string Id { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Course> Courses { get; set; }
    }
}
