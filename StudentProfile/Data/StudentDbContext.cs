using Microsoft.EntityFrameworkCore;
using StudentProfile.Models;

namespace StudentProfile.Data
{
    public class StudentDbContext:DbContext
    {

        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
    }
}
