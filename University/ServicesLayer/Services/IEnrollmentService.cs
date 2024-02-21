namespace ServicesLayer.Services;

public interface IEnrollmentService
{
  public Task EnrollCourseAsync(int courseId, int userId);
  public Task DisenrollCourseAsync(int courseId, int userId);
}