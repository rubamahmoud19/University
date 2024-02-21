using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Dto;

public class UserLoginDto
{

  public string? Username { get; set; }

  public string? Password { get; set; }

}