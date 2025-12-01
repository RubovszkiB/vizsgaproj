using System.ComponentModel.DataAnnotations;

namespace RMBCoachingApp.Models
{
    public class TrainingPlan
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "A cím megadása kötelező")]
        [StringLength(200, ErrorMessage = "A cím maximum 200 karakter lehet")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "A kategória megadása kötelező")]
        public string Category { get; set; }
        
        [Range(1, 52, ErrorMessage = "Az időtartam 1 és 52 hét között lehet")]
        public int DurationWeeks { get; set; }
        
        [Range(0, 1000000, ErrorMessage = "Az ár 0 és 1,000,000 Ft között lehet")]
        public decimal Price { get; set; }
        
        public int TrainerId { get; set; }
        
        public string TrainerName { get; set; }
        
        public string Description { get; set; }
        
        public string DifficultyLevel { get; set; } // Kezdő, Közép, Haladó
        
        public int SessionsPerWeek { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public bool IsPublished { get; set; } = true;
        
        public int PurchaseCount { get; set; } = 0;
    }
}