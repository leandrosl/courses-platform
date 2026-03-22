using CoursesPlatform.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CoursesPlatform.Api.Data;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Course> Courses => Set<Course>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("courses");
            entity.HasKey(course => course.Id).HasName("PK_courses");
            entity.Property(course => course.Title).HasMaxLength(200).IsRequired();
            entity.Property(course => course.Description).HasMaxLength(1000);
            entity.Property(course => course.WorkloadHours).IsRequired();
            entity.Property(course => course.CreatedAtUtc).IsRequired();
        });
    }
}
