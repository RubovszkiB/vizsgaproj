using System.ComponentModel.DataAnnotations;

namespace RMBCoachingApp.Models
{
    public class Athlete
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "A név megadása kötelező")]
        [StringLength(100, ErrorMessage = "A név maximum 100 karakter lehet")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Az email megadása kötelező")]
        [EmailAddress(ErrorMessage = "Érvénytelen email cím")]
        public string Email { get; set; }
        
        [Phone(ErrorMessage = "Érvénytelen telefonszám")]
        public string Phone { get; set; }
        
        public DateTime RegistrationDate { get; set; }
        
        [Range(12, 80, ErrorMessage = "Az életkor 12 és 80 között lehet")]
        public int Age { get; set; }
        
        public string SportType { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public string Address { get; set; }
        
        public string EmergencyContact { get; set; }
        
        public string MedicalNotes { get; set; }
        
        public List<int> PurchasedPlanIds { get; set; } = new List<int>();
        
        public DateTime? LastLoginDate { get; set; }
        
        public string MembershipType { get; set; } // Alap, Prémium, VIP
    }
}