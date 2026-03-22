namespace CoursesPlatform.Api.Models;

public sealed class Course
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int WorkloadHours { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}
