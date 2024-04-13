using Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Repository.Data
{
    public class AppDbContext:DbContext
    {
        
    
    public DbSet<Education> Educations { get; set; }
        public DbSet<Group> Groups { get; set; }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-HMGHF14\\SQLEXPRESS;Database=EntityFrameworkCourseDb;Trusted_Connection=true;TrustServerCertificate=True");
        }
    }
}
