using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementAPI.Data;
using SchoolManagementAPI.Models;


namespace SchoolManagementAPI.Controller;
[ApiController]
[Route("api/[controller]")]
public class StudentCourseController : ControllerBase
{
    private readonly AppDbContext _context;
    
    public StudentCourseController(AppDbContext context)
    {
        _context = context;
    }
    
    //post api/studentcourse
    [HttpPost]
    public async Task<IActionResult> CreateStudentCourse(StudentCourse course)
    {
        var existingStudent = await _context.Students
            .AnyAsync(s => s.Id == course.StudentId);
        if (!existingStudent)
        {
            return BadRequest("Student not found.");
        }

        var existingCourse = await _context.Courses
            .AnyAsync(c => c.Id == course.CourseId);
        if (!existingCourse)
        {
            return BadRequest("Course not found.");
        }

        var alreadyExists = await _context.StudentCourses
            .AnyAsync(sc => sc.StudentId == course.StudentId &&
                            sc.CourseId == course.CourseId);
        if (alreadyExists)
        {
            return BadRequest("Student alrady registered this course.");
        }
        
        _context.StudentCourses.Add(course);
        await _context.SaveChangesAsync();

        return StatusCode(201, course);
    }
    
    // get api/studentcourse/1
    [HttpGet("{studentId}")]
    public async Task<IActionResult> GetStudentCourses(int studentId)
    {
        var studentExists = await _context.Students
            .AnyAsync(s => s.Id == studentId);

        if (!studentExists)
        {
            return NotFound("Student not found.");
        }
        
        var courses = await _context.StudentCourses
            .Where(sc => sc.StudentId == studentId)
            .Select(sc => new
            {
                sc.CourseId,
                CourseName = sc.Course.Name
            })
            .ToListAsync();

        return Ok(courses);
    }
}