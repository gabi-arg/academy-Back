using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using ContactApi.Models;
using ContactApi.Data;
[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly ContactContext _context;

    public ContactController(ContactContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> SendContact([FromBody] ContactForm form)
    {
        _context.ContactForms.Add(form);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Formulario guardado en la base de datos" });
    }
    [HttpGet]
    public async Task<IActionResult> GetMessages()
    {
        var messages = await _context.ContactForms.ToListAsync();
        return Ok(messages);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMessage(int id)
    {
        var message = await _context.ContactForms.FindAsync(id);
        if (message == null) return NotFound();

        _context.ContactForms.Remove(message);
        await _context.SaveChangesAsync();

        return NoContent();
    }


}
