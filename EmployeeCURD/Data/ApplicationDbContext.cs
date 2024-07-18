using EmployeeCURD.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCURD.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public  DbSet <Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
