using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;

using ServicesLayer.Services;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using ServicesLayer;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<CourseService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


builder.Services.AddDbContext<UniversityDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("University"));
});


// builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//     .AddCookie();


// ------ Jwt ---------------
#pragma warning disable CS8604 // Possible null reference argument.
byte[] key = Encoding.ASCII.GetBytes("fvh8456477hth44j6wfds98bq9hp8bqh9ubq9gjig3qr0");
#pragma warning restore CS8604 // Possible null reference argument.
builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
// ------ Jwt ---------------
var configuration = builder.Configuration;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
