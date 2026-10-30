using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementAPI.Data;
using SchoolManagementAPI.Models;
namespace SchoolManagementAPI.Controller;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly AppDbContext _context;
    
    public CourseController(AppDbContext context)
    {
        _context = context;
    }
    
    //post; api/course
    [HttpPost]
    public async Task<IActionResult> CreateCourses(Course course)
    {
        var newCourse = new Course
        {
            Name = course.Name
        };
        _context.Courses.Add(newCourse);
        await _context.SaveChangesAsync();
        return StatusCode(201, newCourse);
    }
    
    //get api/course
    [HttpPost]
    public async Task<IActionResult> GetCourses()
    {
        var courses = await _context.Courses
            .Select(c => new
            {
                c.Id,
                c.Name
            })
            .ToListAsync();
        return Ok(courses);
    }
}