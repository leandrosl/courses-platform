using CoursesPlatform.Api.Contracts;
using CoursesPlatform.Api.Data;
using CoursesPlatform.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoursesPlatform.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CoursesController(ApplicationDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Course>>> GetAll()
    {
        var courses = await dbContext.Courses
            .OrderBy(course => course.Id)
            .ToListAsync();

        return Ok(courses);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Course>> GetById(int id)
    {
        var course = await dbContext.Courses.FindAsync(id);
        if (course is null)
        {
            return NotFound();
        }

        return Ok(course);
    }

    [HttpPost]
    public async Task<ActionResult<Course>> Create([FromBody] SaveCourseRequest request)
    {
        var course = new Course
        {
            Title = request.Title.Trim(),
            Description = request.Description?.Trim(),
            WorkloadHours = request.WorkloadHours,
            CreatedAtUtc = DateTime.UtcNow
        };

        dbContext.Courses.Add(course);
        await dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Course>> Update(int id, [FromBody] SaveCourseRequest request)
    {
        var course = await dbContext.Courses.FindAsync(id);
        if (course is null)
        {
            return NotFound();
        }

        course.Title = request.Title.Trim();
        course.Description = request.Description?.Trim();
        course.WorkloadHours = request.WorkloadHours;

        await dbContext.SaveChangesAsync();

        return Ok(course);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var course = await dbContext.Courses.FindAsync(id);
        if (course is null)
        {
            return NotFound();
        }

        dbContext.Courses.Remove(course);
        await dbContext.SaveChangesAsync();

        return NoContent();
    }
}
