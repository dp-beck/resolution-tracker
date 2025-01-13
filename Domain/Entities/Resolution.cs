﻿using System.Globalization;

namespace Domain.Entities;

public class Resolution
{
   public int Id { get; set; } 
   public required string Title { get; set; }
   public string? Description { get; set; }
   public int? Goal { get; set; }
   public int? CurrentLevel { get; set; } = 0;
   public string? PercentComplete
   {
      get
      {
         if (CurrentLevel != null && Goal != null)
            return ((double)CurrentLevel / (double)Goal).ToString("P", CultureInfo.InvariantCulture);
         return null;
      }
   }

   public bool IsComplete { get; set; }
   public DateTime? CompletedOn { get; set; }
}