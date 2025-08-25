using Microsoft.AspNetCore.Mvc;
using ContactApi.Data;
using ContactApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly StudentContext _context;
    public StudentController(StudentContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CreateStudent(Student student)
    {
        _context.Students.Add(student);
        _context.SaveChanges();
        return Ok(new { message = "Estudiante creado correctamente" });
    }

    [HttpGet]
    public IActionResult GetAllStudents()
    {
        var students = _context.Students.ToList();
        return Ok(students);
    }
    [HttpDelete("{id}")]
    public IActionResult DeleteStudent(int id)
    {
        var student = _context.Students.Find(id);
        if (student == null)
            return NotFound();

        _context.Students.Remove(student);
        _context.SaveChanges();
        return NoContent();
}
    [HttpPut("{id}")]
    public IActionResult UpdateStudent(int id, [FromBody] Student updatedStudent)
    {
        var student = _context.Students.Find(id);
        if (student == null)
            return NotFound();
        student.Name = updatedStudent.Name;
        student.Email = updatedStudent.Email;
        student.Phone = updatedStudent.Phone;
        student.Level = updatedStudent.Level;
        student.Grades = updatedStudent.Grades;

        _context.SaveChanges();
        return Ok(new { message = "Estudiante actualizado correctamente" });
    }
    [HttpGet("check")]
    public async Task<IActionResult> CheckStudentByEmail([FromQuery] string email)
{
    var student = await _context.Students.FirstOrDefaultAsync(s => s.Email == email);

    if (student == null)
    {
        return NotFound(new { message = "No existe el estudiante" });
    }

    return Ok(student);
}

}