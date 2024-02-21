using Microsoft.EntityFrameworkCore;

using EntityLayer.Entities;


namespace DataLayer;

public class UniversityDbContext : DbContext
{
  public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options)
  {

  }
  public DbSet<University> Universities { get; set; }
  public DbSet<Course> Courses { get; set; }
  public DbSet<User> Users { get; set; }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Course>()
        .HasIndex(b => b.CourseName)
        .IsUnique();
  }
}

