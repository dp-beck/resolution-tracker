namespace Domain.Entities;

public class ResolutionCategory
{
    public required string Name { get; set; }
    public Resolution[]? Resolutions { get; set; }
    
}