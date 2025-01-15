namespace Domain.Entities;

public class ResolutionCategory
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public Resolution[]? Resolutions { get; set; }
    
}