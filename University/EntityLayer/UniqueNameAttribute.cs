// using System.ComponentModel.DataAnnotations;

// using DataLayer;
// using EntityLayer.Entities;
// using Microsoft.EntityFrameworkCore;

// // [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
// public class UniqueNameAttribute : ValidationAttribute
// {  
//    private readonly UniversityDbContext _context;
//     public UniqueNameAttribute(UniversityDbContext context)
//     {
//       _context = context;
//     }
// #pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
//     protected override ValidationResult IsValid(object value, ValidationContext validationContext)
// #pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
//     {
//         // var context = (UniversityDbContext)validationContext.GetService(typeof(UniversityDbContext));

//         if (!_context.Courses.Any(a=>a.CourseName==value.ToString()))
//         {
// #pragma warning disable CS8603 // Possible null reference return.
//             return ValidationResult.Success;
// #pragma warning restore CS8603 // Possible null reference return.
//         }
//         return new ValidationResult("Name is exists");

//     }
// }