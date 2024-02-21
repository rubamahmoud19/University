using EntityLayer.Dto;
using AutoMapper;
using EntityLayer.Entities;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

namespace ServicesLayer.Services;


public class CourseService : ICourseService
{
  private readonly UniversityDbContext _context;
  private readonly IMapper _mapper;

  public CourseService(UniversityDbContext context, AutoMapper.IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public string CreateCourse(CourseDto model)
  {
    var course = new Course
    {
      CourseName = model.CourseName
    };
    _context.Courses.Add(course);
    try
    {
      int result = _context.SaveChanges();
      return "success";
    }
    catch (Exception ex)
    {
      if (ex.InnerException is SqliteException sqliteEx && sqliteEx.SqliteErrorCode == 19)
      {
        return "Course name has already been taken";
      }
      return $"{ex.Message}";
    }
  }

  public string DeleteCourse(Course course)
  {
    _context.Remove(course);
    int changesSaved = _context.SaveChanges();
    if (changesSaved > 0)
    {
      return "deleted";
    }
    else
    {
      return "An error occurred while saving the changes";
    }
  }

  public async Task<string> UpdateCourse(int id, CourseDto updatedCourseDto)
  {
    var existingCourse = await _context.Courses.FindAsync(id);
    if (existingCourse != null)
    {
      existingCourse.CourseName = updatedCourseDto.CourseName;
      try
      {
        int result = _context.SaveChanges();
        return "success";
      }
      catch (Exception ex)
      {
        if (ex.InnerException is SqliteException sqliteEx && sqliteEx.SqliteErrorCode == 19)
        {
          return "Course name has already been taken";
        }
        return $"{ex.Message}";
      }
    }else{
      return "Not Found";
    }

  }

  public CourseDto GetCourse(int id)
  {
    Course? course = _context.Courses.Include(c => c.Users).FirstOrDefault(c => c.Id == id);
    var courseDto = _mapper.Map<CourseDto>(course);
    return courseDto;
  }

  List<Course> ICourseService.GetCourses(string searchString)
  {
    if (!String.IsNullOrEmpty(searchString))
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
      return (List<Course>)_context.Courses.Where(s => s.CourseName.Contains(searchString)).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }
    else
    {
      return _context.Courses.ToList();
    }
  }

  public Course GetCourseWithStudents(int id)
  {
    var course = _context.Courses.Include(c => c.Users).FirstOrDefault(c => c.Id == id);
#pragma warning disable CS8603 // Possible null reference return.
    return course;
#pragma warning restore CS8603 // Possible null reference return.
  }

}