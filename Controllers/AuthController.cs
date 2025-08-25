using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ContactApi.Data; 
using ContactApi.Models;
using ContactApi.DTOs;
[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;

    public AuthController(AppDbContext context) {
        _context = context;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterDto dto) {
        var existing = _context.Users.FirstOrDefault(u => u.Username == dto.Username);
    if (existing != null)
        return BadRequest(new { message = "El usuario ya existe" });

        var user = new User {
            Username = dto.Username,
            Password = dto.Password,
            Email = dto.Email, 
            Role = dto.Role
        };

        _context.Users.Add(user);
        _context.SaveChanges();

       
        return Ok(new { message = "Registro exitoso" });

    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto dto) {
        var user = _context.Users.FirstOrDefault(u =>
            u.Email == dto.Email && u.Password == dto.Password);

        if (user == null)
            return Unauthorized("Credenciales inválidas");

        return Ok(new {
            username = user.Username,
             email = user.Email,
            role = user.Role
        });
    }
}
