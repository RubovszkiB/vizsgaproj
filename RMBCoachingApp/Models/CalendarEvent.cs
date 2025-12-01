using System.ComponentModel.DataAnnotations;

namespace RMBCoachingApp.Models
{
    public class CalendarEvent
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "A cím megadása kötelező")]
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        [Required]
        public DateTime EventDate { get; set; }
        
        public TimeSpan StartTime { get; set; }
        
        public TimeSpan EndTime { get; set; }
        
        public string Location { get; set; }
        
        public string EventType { get; set; } // Edzés, Találkozó, Esemény, Játék
        
        public List<int> ParticipantIds { get; set; } = new List<int>();
        
        public string Participants { get; set; }
        
        public int TrainerId { get; set; }
        
        public string TrainerName { get; set; }
        
        public bool IsCompleted { get; set; } = false;
        
        public string Notes { get; set; }
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        // Naptár megjelenítéshez
        public string DisplayTime => $"{StartTime:hh\\:mm} - {EndTime:hh\\:mm}";
    }
}