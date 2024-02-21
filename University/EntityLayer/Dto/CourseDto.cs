using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Dto;

public class CourseDto
{ 
  public int Id { get; set; }
  [Required]
  public string? CourseName { get; set; }
    
}
