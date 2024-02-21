using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using DataLayer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;

public class UsersController : Controller
{
    private readonly UniversityDbContext _context;
    // private readonly AuthService _authservice; 

    public UsersController(UniversityDbContext context)
    {
        _context = context;
        // _authservice = authservice;
    }

    public IActionResult Index()
    {

        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(User model)
    {
        // Validate user credentials against the database
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == model.Username);
        if (user != null && user.Username != null)
        {
            // User is authenticated; generate a JWT token
            var token = GenerateJwtToken(user.Username);

            // Set the token in a cookie
            HttpContext.Session.SetString("JwtToken", token);

            return RedirectToAction("Index", "Universities");
        }

        return RedirectToAction("Index", "Users");
    }

    private string GenerateJwtToken(string email)
    {
        if (!string.IsNullOrEmpty(email))
        {
            byte[] key = Encoding.ASCII.GetBytes("fvh8456477hth44j6wfds98bq9hp8bqh9ubq9gjig3qr0"); // Change this to a secure key
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, email) }),
                Expires = DateTime.UtcNow.AddHours(1), // Set the expiration time as needed
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        return "";
    }
}
