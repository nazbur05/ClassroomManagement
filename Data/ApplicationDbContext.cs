using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ClassroomManagement.Models;

namespace ClassroomManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<CourseMaterial> CourseMaterials { get; set; }
        public DbSet<CourseMaterialFile> CourseMaterialFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
            .Property(u => u.StudId)
            .HasMaxLength(6);

            builder.Entity<StudentCourse>()
                .HasKey(ss => new { ss.StudentId, ss.CourseId });

            builder.Entity<StudentCourse>()
                .HasOne(ss => ss.Student)
                .WithMany(u => u.StudentCourses)
                .HasForeignKey(ss => ss.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<StudentCourse>()
                .HasOne(ss => ss.Course)
                .WithMany(s => s.StudentCourse)
                .HasForeignKey(ss => ss.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

             builder.Entity<CourseMaterial>()
            .HasOne(cm => cm.Course)
            .WithMany(c => c.Materials)
            .HasForeignKey(cm => cm.CourseId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}