using DataLayer;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using ServicesLayer.Services;

public class CourseEnrollmentService : IEnrollmentService
{
    private readonly UniversityDbContext _context;

    public CourseEnrollmentService(UniversityDbContext context)
    {
        _context = context;
    }

    public async Task EnrollCourseAsync(int courseId, int userId)
    {
        var course = await _context.Courses.FindAsync(courseId);
        var user = await _context.Users.FindAsync(userId);

        if (course == null || user == null)
        {
            throw new ArgumentException("Invalid course or user ID.");
        }

        user.Courses ??= new List<Course>();
        user.Courses.Add(course);

        _ = await _context.SaveChangesAsync();
    }

    public async Task DisenrollCourseAsync(int courseId, int userId)
    {
        var user = await _context.Users
            .Include(u => u.Courses)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            throw new ArgumentException("Invalid user ID.");
        }else {
        var course = user.Courses?.FirstOrDefault(c => c.Id == courseId);

        if (course != null)
        {
          user.Courses.Remove(course);
          await _context.SaveChangesAsync();
        }
        }
    }
}