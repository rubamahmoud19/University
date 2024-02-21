using System.ComponentModel.DataAnnotations;
// using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using EntityLayer.Enums;


namespace EntityLayer.Entities;

public class User
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int Id { get; set; }
  public string? Name { get; set; }
  public string? Username { get; set; }

  public string? Password { get; set; }
  [Required]
  public UserType UserType { get; set; }

  public virtual ICollection<Course>? Courses { get; set; }

}