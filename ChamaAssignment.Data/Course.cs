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
    
    public partial class Course
    {
        public Course()
        {
            this.Students = new HashSet<Student>();
        }
    
        public string Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public string Lecturer { get; set; }
    
        public virtual ICollection<Student> Students { get; set; }
    }
}