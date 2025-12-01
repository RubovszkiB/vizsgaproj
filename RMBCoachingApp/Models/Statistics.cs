namespace RMBCoachingApp.Models
{
    public class Statistics
    {
        public int TotalTrainers { get; set; }
        public double AvgExperienceYears { get; set; }
        public int TotalAthletes { get; set; }
        public int ActiveAthletes { get; set; }
        public double AvgAthleteAge { get; set; }
        public int NewAthletesThisMonth { get; set; }
        
        public int TotalTrainingPlans { get; set; }
        public decimal AvgPlanPrice { get; set; }
        public double AvgPlanDuration { get; set; }
        public string MostPopularCategory { get; set; }
        
        public decimal TotalRevenue { get; set; }
        public decimal MonthlyRevenue { get; set; }
        public decimal AvgPurchaseValue { get; set; }
        public string MostSoldPlan { get; set; }
        
        public Dictionary<string, int> CategoryDistribution { get; set; } = new();
        public Dictionary<string, int> SportTypeDistribution { get; set; } = new();
        public List<TopTrainer> TopTrainers { get; set; } = new();
    }

    public class TopTrainer
    {
        public string Name { get; set; }
        public int AthleteCount { get; set; }
        public int PlanCount { get; set; }
        public double Rating { get; set; }
    }
}