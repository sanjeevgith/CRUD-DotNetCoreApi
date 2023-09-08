using DotNetCrudApiLatest.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetCrudApiLatest.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}
 