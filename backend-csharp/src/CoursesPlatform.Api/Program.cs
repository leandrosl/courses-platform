using CoursesPlatform.Api.Data;
using CoursesPlatform.Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection is not configured.");

builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await dbContext.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

var coursesGroup = app.MapGroup("/api/courses");

coursesGroup.MapGet("/", async (ApplicationDbContext dbContext) =>
{
    var courses = await dbContext.Courses
        .OrderBy(course => course.Id)
        .ToListAsync();

    return Results.Ok(courses);
});

coursesGroup.MapGet("/{id:int}", async (int id, ApplicationDbContext dbContext) =>
{
    var course = await dbContext.Courses.FindAsync(id);
    return course is null ? Results.NotFound() : Results.Ok(course);
});

coursesGroup.MapPost("/", async (Course input, ApplicationDbContext dbContext) =>
{
    var validationErrors = ValidateCourse(input);
    if (validationErrors.Count > 0)
    {
        return Results.ValidationProblem(validationErrors);
    }

    var course = new Course
    {
        Title = input.Title.Trim(),
        Description = input.Description?.Trim(),
        WorkloadHours = input.WorkloadHours,
        CreatedAtUtc = DateTime.UtcNow
    };

    dbContext.Courses.Add(course);
    await dbContext.SaveChangesAsync();

    return Results.Created($"/api/courses/{course.Id}", course);
});

coursesGroup.MapPut("/{id:int}", async (int id, Course input, ApplicationDbContext dbContext) =>
{
    var course = await dbContext.Courses.FindAsync(id);
    if (course is null)
    {
        return Results.NotFound();
    }

    var validationErrors = ValidateCourse(input);
    if (validationErrors.Count > 0)
    {
        return Results.ValidationProblem(validationErrors);
    }

    course.Title = input.Title.Trim();
    course.Description = input.Description?.Trim();
    course.WorkloadHours = input.WorkloadHours;

    await dbContext.SaveChangesAsync();
    return Results.Ok(course);
});

coursesGroup.MapDelete("/{id:int}", async (int id, ApplicationDbContext dbContext) =>
{
    var course = await dbContext.Courses.FindAsync(id);
    if (course is null)
    {
        return Results.NotFound();
    }

    dbContext.Courses.Remove(course);
    await dbContext.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();

static Dictionary<string, string[]> ValidateCourse(Course input)
{
    var errors = new Dictionary<string, string[]>();

    if (string.IsNullOrWhiteSpace(input.Title))
    {
        errors["title"] = ["Title is required."];
    }

    if (input.WorkloadHours <= 0)
    {
        errors["workloadHours"] = ["WorkloadHours must be greater than zero."];
    }

    return errors;
}
