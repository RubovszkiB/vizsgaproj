using System.ComponentModel.DataAnnotations;

namespace RMBCoachingApp.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "A név megadása kötelező")]
        [StringLength(100, ErrorMessage = "A név maximum 100 karakter lehet")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "A szakértelem megadása kötelező")]
        public string Specialty { get; set; }
        
        [Range(0, 50, ErrorMessage = "A tapasztalat 0 és 50 év között lehet")]
        public int ExperienceYears { get; set; }
        
        [Range(0, 1000, ErrorMessage = "A képzett sportolók száma 0 és 1000 között lehet")]
        public int TrainedAthletes { get; set; }
        
        public string Description { get; set; }
        
        [EmailAddress(ErrorMessage = "Érvénytelen email cím")]
        public string Email { get; set; }
        
        [Phone(ErrorMessage = "Érvénytelen telefonszám")]
        public string Phone { get; set; }
        
        public DateTime HireDate { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public double Rating { get; set; } = 0.0;
    }
}