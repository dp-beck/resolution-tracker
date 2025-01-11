using System.Globalization;

namespace Domain.Entities;

public class Resolution
{
   public int Id { get; set; } 
   public string? Title { get; set; }
   public string? Description { get; set; }
   public int Goal { get; set; }
   public int CurrentLevel { get; set; }
   public string PercentComplete => ((double)CurrentLevel / (double)Goal).ToString("P", CultureInfo.InvariantCulture);
}