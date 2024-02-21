using Microsoft.AspNetCore.Mvc;
using DataLayer;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using EntityLayer.Dto;
using ServicesLayer.Services;
using AutoMapper;

namespace Controllers
{
  public class CoursesController : Controller
  {
    private readonly UniversityDbContext _context;
    private readonly ICourseService _courseService;
    private readonly IMapper _mapper;


    public CoursesController(UniversityDbContext context, CourseService courseService, IMapper mapper)
    {
      _context = context;
      _courseService = courseService;
      _mapper = mapper;
    }
    public IActionResult Index(string searchString)
    {
      ViewBag.courses = _courseService.GetCourses(searchString);
      return View();
    }
    [HttpGet]
    public IActionResult New()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Create(CourseDto course)
    {
      if (ModelState.IsValid)
      {
        string result = _courseService.CreateCourse(course);
        if (result == "success")
        {
          return RedirectToAction("Index", "Courses");
        }
        else
        {
          ViewData["ErrorMessage"] = $"{result}";
          return View("New", course);
        }
      }
      else
      {
        return View("New", course);
      }
    }

    public IActionResult Edit(int id)
    {
      var  existingCourse = _courseService.GetCourse(id);
      if (existingCourse == null)
      {
        return RedirectToAction("index");
      }
      else
      {
        return View(existingCourse);
      }
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, CourseDto updatedCourseDto)
    {
      var existingCourse = await _context.Courses.FindAsync(id);
      if (ModelState.IsValid)
      {
        if (existingCourse == null)
        {
          // Course with the given ID does not exist
          // throw new ArgumentException("Course not found");
          return RedirectToAction("index");
        }

        // Update properties from the DTO
        existingCourse.CourseName = updatedCourseDto.CourseName;

        // Save changes to the database
        await _context.SaveChangesAsync();
      }
      return RedirectToAction("index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
      var course = await _context.Courses.FindAsync(id);

      if (course == null)
      {
        return RedirectToAction("index");
      }
      else
      {
        return View(course);
      }

    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
      Course? course = _context.Courses.Find(id);
      if (course != null)
      {
        var DeleteSuccessfully = _courseService.DeleteCourse(course);
        if (DeleteSuccessfully == "deleted")
        {
          ViewData["notice"] = "deleted successfully";
        }
        else
        {
          ViewData["notice"] = "Error occured while deleting it";
        }
      }
      else
      {
        ViewData["notice"] = "this course is not exist";
      }
      return RedirectToAction("Index");
    }

    public IActionResult Show(int id)
    {
      var course = _courseService.GetCourseWithStudents(id);
      if (course == null)
      {
        return RedirectToAction("Index");
      }
      ViewBag.course = course;
      return View(course);
    }
  }
}
