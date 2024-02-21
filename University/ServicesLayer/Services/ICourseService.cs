using EntityLayer.Dto;
using EntityLayer.Entities;

namespace ServicesLayer.Services;

public interface ICourseService
{
    public string CreateCourse(CourseDto course);
    public CourseDto GetCourse(int id);
    public Course GetCourseWithStudents(int id);
    public List<Course> GetCourses(String searchString);
    public Task<string> UpdateCourse(int id, CourseDto course);
    public string DeleteCourse(Course course);
}