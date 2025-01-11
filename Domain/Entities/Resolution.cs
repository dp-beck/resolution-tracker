namespace Domain.Entities;

public class Resolution
{
   public int Id { get; set; } 
   public string? Title { get; set; }
   public string? Description { get; set; }
   public int Goal { get; set; }
   public int CurrentLevel { get; set; }
   public double PercentComplete => ((double)CurrentLevel / (double)Goal) * 100;
}