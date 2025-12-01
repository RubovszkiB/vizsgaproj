using System.ComponentModel.DataAnnotations;

namespace RMBCoachingApp.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        
        [Required]
        public int AthleteId { get; set; }
        
        public string AthleteName { get; set; }
        
        [Required]
        public int TrainingPlanId { get; set; }
        
        public string TrainingPlanTitle { get; set; }
        
        public DateTime PurchaseDate { get; set; }
        
        [Range(0, 1000000)]
        public decimal Price { get; set; }
        
        public string PaymentMethod { get; set; } // Készpénz, Bankkártya, Banki átutalás
        
        public bool IsPaid { get; set; } = true;
        
        public string InvoiceNumber { get; set; }
        
        public string Notes { get; set; }
        
        public string Status { get; set; } // Függőben, Teljesítve, Lemondva
    }
}