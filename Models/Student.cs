using System.ComponentModel.DataAnnotations.Schema;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    public int? Grades { get; set; } 
}