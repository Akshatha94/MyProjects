using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaAssignment.Data
{
    public interface IDbContext : IDisposable
    {
        DbSet<Course> Courses { get; set; }
        DbSet<Student> Students { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
