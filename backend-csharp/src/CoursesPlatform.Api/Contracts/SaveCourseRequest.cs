using System.ComponentModel.DataAnnotations;

namespace CoursesPlatform.Api.Contracts;

public sealed class SaveCourseRequest
{
    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    [Range(1, int.MaxValue)]
    public int WorkloadHours { get; set; }
}
