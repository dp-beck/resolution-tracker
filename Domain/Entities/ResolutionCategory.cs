using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class ResolutionCategory()
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Resolution>? Resolutions { get; set; } = new List<Resolution>();
}