using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementAPI.Data;
using SchoolManagementAPI.Models;
namespace SchoolManagementAPI.Controller;

[ApiController]
[Route("api/[controller]")]

public class StudentController : ControllerBase
{
    private readonly AppDbContext _context;

    public StudentController(AppDbContext context)
    {
        _context = context;
    }
    
    // post; api/student
    [HttpPost]
    public async Task<IActionResult> CreateStudent(Student student)
    {
        var classroomExists = await _context.Classrooms
            .AnyAsync(c => c.Id == student.ClassroomId);
        if (!classroomExists)
        {
            return BadRequest("Classroom not found");
        }

        var newStudent = new Student()
        {
            FullName = student.FullName,
            Age = student.Age,
            ClassroomId = student.ClassroomId
        };
       
        _context.Students.Add(newStudent);
        await _context.SaveChangesAsync();

        return StatusCode(201, newStudent);
    }
    
    // get; api/student
    [HttpGet]
    public async Task<IActionResult> GetStudents()
    {
        var students = await _context.Students
            .Include(s => s.Classroom)
            .Select(s => new
            {
                s.Id,
                s.FullName,
                s.Age,
                Classroom = new
                {
                    s.Classroom.Id,
                    s.Classroom.Name
                }
            })
            .ToListAsync();

        return Ok(students);
    }
    
}