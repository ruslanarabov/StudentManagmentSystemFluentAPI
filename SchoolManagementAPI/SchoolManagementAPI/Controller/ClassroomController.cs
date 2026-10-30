using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementAPI.Data;
using SchoolManagementAPI.Models;
namespace SchoolManagementAPI.Controller;

[ApiController]
[Route("api/[controller]")]
public class ClassroomController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClassroomController(AppDbContext context)
    {
        _context = context;
    }
    
    // post; api/classroom
    [HttpPost]
    public async Task<IActionResult> CreateClassroom(Classroom classroom)
    {
        var newClassroom = new Classroom
        {
            Name = classroom.Name
        };
        
        _context.Classrooms.Add(newClassroom);
        await _context.SaveChangesAsync();
        return StatusCode(201, newClassroom);
    }
    
    // get; api/classorm
    [HttpGet]
    public async Task<IActionResult> GetClassrooms()
    {
        var classrooms = await _context.Classrooms
            .Select(c => new
            {
                c.Id,
                c.Name
            })
            .ToListAsync();

        return Ok(classrooms);
    }

}