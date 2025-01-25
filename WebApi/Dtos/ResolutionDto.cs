namespace WebApplication1.Dtos;

public class ResolutionDto
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public int? Goal { get; set; }
    public int? CurrentLevel { get; set; } = 0;
    public string? PercentComplete { get; set; }
    public bool IsComplete { get; set; }
    public DateTime? CompletedOn { get; set; }
    public int CategoryId { get; set; }
    public ResolutionCategoryDto? Category { get; set; }
}