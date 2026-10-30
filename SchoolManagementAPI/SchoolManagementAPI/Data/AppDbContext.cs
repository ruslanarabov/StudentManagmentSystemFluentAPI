using Microsoft.EntityFrameworkCore;
using SchoolManagementAPI.Models;

namespace SchoolManagementAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Classroom> Classrooms { get; set; }
    public DbSet<StudentCourse> StudentCourses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Student config
        modelBuilder.Entity<Student>()
            .Property(s => s.FullName)
            .HasMaxLength(100);

        modelBuilder.Entity<Student>()
            .Property(s => s.Age)
            .HasColumnType("integer");

        // Classroom config
        modelBuilder.Entity<Classroom>()
            .Property(c => c.Name)
            .HasMaxLength(50);

        // Course config
        modelBuilder.Entity<Course>()
            .Property(c => c.Name)
            .HasMaxLength(100);

        // One to many Classroom - Students
        modelBuilder.Entity<Student>()
            .HasOne(s => s.Classroom)
            .WithMany(c => c.Students)
            .HasForeignKey(s => s.ClassroomId);

        // Many to Many Student Course
        modelBuilder.Entity<StudentCourse>()
            .HasKey(sc => new { sc.StudentId, sc.CourseId });

        modelBuilder.Entity<StudentCourse>()
            .HasOne(sc => sc.Student)
            .WithMany()
            .HasForeignKey(sc => sc.StudentId);

        modelBuilder.Entity<StudentCourse>()
            .HasOne(sc => sc.Course)
            .WithMany()
            .HasForeignKey(sc => sc.CourseId);
    }
 }