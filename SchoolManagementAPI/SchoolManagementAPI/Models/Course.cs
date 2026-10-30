namespace SchoolManagementAPI.Models;

public sealed class Course
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public ICollection<Student> Students { get; set; } = new List<Student>();
}