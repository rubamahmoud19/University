using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
  public class EnrollementsController : Controller
  {
    [HttpPost]
    public IActionResult Enroll(int courseId){
      return View();
    }

    [HttpPost]
    public IActionResult Disenroll(int courseId){
      return View();
    }

  }
}