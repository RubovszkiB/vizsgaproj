using RMBCoachingApp.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace RMBCoachingApp.Services
{
    public class DatabaseService
    {
        private string connectionString;
        
        public DatabaseService()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        
        public bool TestConnection()
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return connection.State == ConnectionState.Open;
                }
            }
            catch
            {
                return false;
            }
        }
        
        // Edzők kezelése
        public List<Trainer> GetTrainers()
        {
            var trainers = new List<Trainer>();
            
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("SELECT * FROM Trainers ORDER BY Name", connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        trainers.Add(new Trainer
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            Specialty = reader["Specialty"].ToString(),
                            ExperienceYears = (int)reader["ExperienceYears"],
                            TrainedAthletes = (int)reader["TrainedAthletes"],
                            Description = reader["Description"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            HireDate = (DateTime)reader["HireDate"],
                            IsActive = (bool)reader["IsActive"],
                            Rating = Convert.ToDouble(reader["Rating"])
                        });
                    }
                }
            }
            
            return trainers;
        }
        
        public bool AddTrainer(Trainer trainer)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(
                    @"INSERT INTO Trainers (Name, Specialty, ExperienceYears, TrainedAthletes, 
                                           Description, Email, Phone, HireDate, IsActive, Rating)
                      VALUES (@Name, @Specialty, @ExperienceYears, @TrainedAthletes, 
                              @Description, @Email, @Phone, @HireDate, @IsActive, @Rating)", connection))
                {
                    command.Parameters.AddWithValue("@Name", trainer.Name);
                    command.Parameters.AddWithValue("@Specialty", trainer.Specialty);
                    command.Parameters.AddWithValue("@ExperienceYears", trainer.ExperienceYears);
                    command.Parameters.AddWithValue("@TrainedAthletes", trainer.TrainedAthletes);
                    command.Parameters.AddWithValue("@Description", trainer.Description ?? "");
                    command.Parameters.AddWithValue("@Email", trainer.Email ?? "");
                    command.Parameters.AddWithValue("@Phone", trainer.Phone ?? "");
                    command.Parameters.AddWithValue("@HireDate", DateTime.Now);
                    command.Parameters.AddWithValue("@IsActive", true);
                    command.Parameters.AddWithValue("@Rating", 0.0);
                    
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                return false;
            }
        }
        
        public bool UpdateTrainer(Trainer trainer)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(
                    @"UPDATE Trainers SET 
                        Name = @Name, 
                        Specialty = @Specialty, 
                        ExperienceYears = @ExperienceYears, 
                        TrainedAthletes = @TrainedAthletes,
                        Description = @Description,
                        Email = @Email,
                        Phone = @Phone,
                        IsActive = @IsActive,
                        Rating = @Rating
                      WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", trainer.Id);
                    command.Parameters.AddWithValue("@Name", trainer.Name);
                    command.Parameters.AddWithValue("@Specialty", trainer.Specialty);
                    command.Parameters.AddWithValue("@ExperienceYears", trainer.ExperienceYears);
                    command.Parameters.AddWithValue("@TrainedAthletes", trainer.TrainedAthletes);
                    command.Parameters.AddWithValue("@Description", trainer.Description ?? "");
                    command.Parameters.AddWithValue("@Email", trainer.Email ?? "");
                    command.Parameters.AddWithValue("@Phone", trainer.Phone ?? "");
                    command.Parameters.AddWithValue("@IsActive", trainer.IsActive);
                    command.Parameters.AddWithValue("@Rating", trainer.Rating);
                    
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                return false;
            }
        }
        
        public bool DeleteTrainer(int id)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("DELETE FROM Trainers WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                return false;
            }
        }
        
        // Edzéstervek kezelése
        public List<TrainingPlan> GetTrainingPlans()
        {
            var plans = new List<TrainingPlan>();
            
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(
                @"SELECT tp.*, t.Name as TrainerName 
                  FROM TrainingPlans tp
                  LEFT JOIN Trainers t ON tp.TrainerId = t.Id
                  ORDER BY tp.CreatedDate DESC", connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        plans.Add(new TrainingPlan
                        {
                            Id = (int)reader["Id"],
                            Title = reader["Title"].ToString(),
                            Category = reader["Category"].ToString(),
                            DurationWeeks = (int)reader["DurationWeeks"],
                            Price = (decimal)reader["Price"],
                            TrainerId = (int)reader["TrainerId"],
                            TrainerName = reader["TrainerName"].ToString(),
                            Description = reader["Description"].ToString(),
                            DifficultyLevel = reader["DifficultyLevel"].ToString(),
                            SessionsPerWeek = (int)reader["SessionsPerWeek"],
                            CreatedDate = (DateTime)reader["CreatedDate"],
                            IsPublished = (bool)reader["IsPublished"],
                            PurchaseCount = (int)reader["PurchaseCount"]
                        });
                    }
                }
            }
            
            return plans;
        }
        
        public bool AddTrainingPlan(TrainingPlan plan)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(
                    @"INSERT INTO TrainingPlans 
                      (Title, Category, DurationWeeks, Price, TrainerId, Description, 
                       DifficultyLevel, SessionsPerWeek, CreatedDate, IsPublished, PurchaseCount)
                      VALUES (@Title, @Category, @DurationWeeks, @Price, @TrainerId, @Description,
                              @DifficultyLevel, @SessionsPerWeek, @CreatedDate, @IsPublished, 0)", connection))
                {
                    command.Parameters.AddWithValue("@Title", plan.Title);
                    command.Parameters.AddWithValue("@Category", plan.Category);
                    command.Parameters.AddWithValue("@DurationWeeks", plan.DurationWeeks);
                    command.Parameters.AddWithValue("@Price", plan.Price);
                    command.Parameters.AddWithValue("@TrainerId", plan.TrainerId);
                    command.Parameters.AddWithValue("@Description", plan.Description ?? "");
                    command.Parameters.AddWithValue("@DifficultyLevel", plan.DifficultyLevel ?? "Közép");
                    command.Parameters.AddWithValue("@SessionsPerWeek", plan.SessionsPerWeek);
                    command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    command.Parameters.AddWithValue("@IsPublished", true);
                    
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                return false;
            }
        }
        
        public bool UpdateTrainingPlan(TrainingPlan plan)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(
                    @"UPDATE TrainingPlans SET 
                        Title = @Title,
                        Category = @Category,
                        DurationWeeks = @DurationWeeks,
                        Price = @Price,
                        TrainerId = @TrainerId,
                        Description = @Description,
                        DifficultyLevel = @DifficultyLevel,
                        SessionsPerWeek = @SessionsPerWeek,
                        IsPublished = @IsPublished
                      WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", plan.Id);
                    command.Parameters.AddWithValue("@Title", plan.Title);
                    command.Parameters.AddWithValue("@Category", plan.Category);
                    command.Parameters.AddWithValue("@DurationWeeks", plan.DurationWeeks);
                    command.Parameters.AddWithValue("@Price", plan.Price);
                    command.Parameters.AddWithValue("@TrainerId", plan.TrainerId);
                    command.Parameters.AddWithValue("@Description", plan.Description ?? "");
                    command.Parameters.AddWithValue("@DifficultyLevel", plan.DifficultyLevel ?? "Közép");
                    command.Parameters.AddWithValue("@SessionsPerWeek", plan.SessionsPerWeek);
                    command.Parameters.AddWithValue("@IsPublished", plan.IsPublished);
                    
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                return false;
            }
        }
        
        public bool DeleteTrainingPlan(int id)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("DELETE FROM TrainingPlans WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                return false;
            }
        }
        
        // Sportolók kezelése
        public List<Athlete> GetAthletes()
        {
            var athletes = new List<Athlete>();
            
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("SELECT * FROM Athletes ORDER BY Name", connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        athletes.Add(new Athlete
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            RegistrationDate = (DateTime)reader["RegistrationDate"],
                            Age = (int)reader["Age"],
                            SportType = reader["SportType"].ToString(),
                            IsActive = (bool)reader["IsActive"],
                            Address = reader["Address"].ToString(),
                            EmergencyContact = reader["EmergencyContact"].ToString(),
                            MedicalNotes = reader["MedicalNotes"].ToString(),
                            LastLoginDate = reader["LastLoginDate"] as DateTime?,
                            MembershipType = reader["MembershipType"].ToString()
                        });
                    }
                }
            }
            
            return athletes;
        }
        
        public bool AddAthlete(Athlete athlete)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(
                    @"INSERT INTO Athletes 
                      (Name, Email, Phone, RegistrationDate, Age, SportType, IsActive, 
                       Address, EmergencyContact, MedicalNotes, LastLoginDate, MembershipType)
                      VALUES (@Name, @Email, @Phone, @RegistrationDate, @Age, @SportType, @IsActive,
                              @Address, @EmergencyContact, @MedicalNotes, @LastLoginDate, @MembershipType)", connection))
                {
                    command.Parameters.AddWithValue("@Name", athlete.Name);
                    command.Parameters.AddWithValue("@Email", athlete.Email);
                    command.Parameters.AddWithValue("@Phone", athlete.Phone ?? "");
                    command.Parameters.AddWithValue("@RegistrationDate", DateTime.Now);
                    command.Parameters.AddWithValue("@Age", athlete.Age);
                    command.Parameters.AddWithValue("@SportType", athlete.SportType ?? "Általános");
                    command.Parameters.AddWithValue("@IsActive", true);
                    command.Parameters.AddWithValue("@Address", athlete.Address ?? "");
                    command.Parameters.AddWithValue("@EmergencyContact", athlete.EmergencyContact ?? "");
                    command.Parameters.AddWithValue("@MedicalNotes", athlete.MedicalNotes ?? "");
                    command.Parameters.AddWithValue("@LastLoginDate", DBNull.Value);
                    command.Parameters.AddWithValue("@MembershipType", "Alap");
                    
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                return false;
            }
        }
        
        public bool UpdateAthlete(Athlete athlete)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(
                    @"UPDATE Athletes SET 
                        Name = @Name,
                        Email = @Email,
                        Phone = @Phone,
                        Age = @Age,
                        SportType = @SportType,
                        IsActive = @IsActive,
                        Address = @Address,
                        EmergencyContact = @EmergencyContact,
                        MedicalNotes = @MedicalNotes,
                        MembershipType = @MembershipType
                      WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", athlete.Id);
                    command.Parameters.AddWithValue("@Name", athlete.Name);
                    command.Parameters.AddWithValue("@Email", athlete.Email);
                    command.Parameters.AddWithValue("@Phone", athlete.Phone ?? "");
                    command.Parameters.AddWithValue("@Age", athlete.Age);
                    command.Parameters.AddWithValue("@SportType", athlete.SportType ?? "Általános");
                    command.Parameters.AddWithValue("@IsActive", athlete.IsActive);
                    command.Parameters.AddWithValue("@Address", athlete.Address ?? "");
                    command.Parameters.AddWithValue("@EmergencyContact", athlete.EmergencyContact ?? "");
                    command.Parameters.AddWithValue("@MedicalNotes", athlete.MedicalNotes ?? "");
                    command.Parameters.AddWithValue("@MembershipType", athlete.MembershipType ?? "Alap");
                    
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                return false;
            }
        }
        
        public bool DeleteAthlete(int id)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("DELETE FROM Athletes WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                return false;
            }
        }
        
        // Vásárlások kezelése
        public List<Purchase> GetPurchases(DateTime? fromDate = null, DateTime? toDate = null)
        {
            var purchases = new List<Purchase>();
            var query = @"SELECT p.*, a.Name as AthleteName, tp.Title as TrainingPlanTitle 
                          FROM Purchases p
                          LEFT JOIN Athletes a ON p.AthleteId = a.Id
                          LEFT JOIN TrainingPlans tp ON p.TrainingPlanId = tp.Id
                          WHERE 1=1";
            
            if (fromDate.HasValue)
                query += " AND p.PurchaseDate >= @FromDate";
            if (toDate.HasValue)
                query += " AND p.PurchaseDate <= @ToDate";
                
            query += " ORDER BY p.PurchaseDate DESC";
            
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                if (fromDate.HasValue)
                    command.Parameters.AddWithValue("@FromDate", fromDate.Value);
                if (toDate.HasValue)
                    command.Parameters.AddWithValue("@ToDate", toDate.Value);
                    
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        purchases.Add(new Purchase
                        {
                            Id = (int)reader["Id"],
                            AthleteId = (int)reader["AthleteId"],
                            AthleteName = reader["AthleteName"].ToString(),
                            TrainingPlanId = (int)reader["TrainingPlanId"],
                            TrainingPlanTitle = reader["TrainingPlanTitle"].ToString(),
                            PurchaseDate = (DateTime)reader["PurchaseDate"],
                            Price = (decimal)reader["Price"],
                            PaymentMethod = reader["PaymentMethod"].ToString(),
                            IsPaid = (bool)reader["IsPaid"],
                            InvoiceNumber = reader["InvoiceNumber"].ToString(),
                            Notes = reader["Notes"].ToString(),
                            Status = reader["Status"].ToString()
                        });
                    }
                }
            }
            
            return purchases;
        }
        
        // Naptár események
        public List<CalendarEvent> GetCalendarEvents(DateTime month)
        {
            var events = new List<CalendarEvent>();
            var startDate = new DateTime(month.Year, month.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(
                @"SELECT ce.*, t.Name as TrainerName 
                  FROM CalendarEvents ce
                  LEFT JOIN Trainers t ON ce.TrainerId = t.Id
                  WHERE ce.EventDate BETWEEN @StartDate AND @EndDate
                  ORDER BY ce.EventDate, ce.StartTime", connection))
            {
                command.Parameters.AddWithValue("@StartDate", startDate);
                command.Parameters.AddWithValue("@EndDate", endDate);
                
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        events.Add(new CalendarEvent
                        {
                            Id = (int)reader["Id"],
                            Title = reader["Title"].ToString(),
                            Description = reader["Description"].ToString(),
                            EventDate = (DateTime)reader["EventDate"],
                            StartTime = (TimeSpan)reader["StartTime"],
                            EndTime = (TimeSpan)reader["EndTime"],
                            Location = reader["Location"].ToString(),
                            EventType = reader["EventType"].ToString(),
                            Participants = reader["Participants"].ToString(),
                            TrainerId = (int)reader["TrainerId"],
                            TrainerName = reader["TrainerName"].ToString(),
                            IsCompleted = (bool)reader["IsCompleted"],
                            Notes = reader["Notes"].ToString(),
                            CreatedDate = (DateTime)reader["CreatedDate"]
                        });
                    }
                }
            }
            
            return events;
        }
        
        public bool AddCalendarEvent(CalendarEvent calendarEvent)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(
                    @"INSERT INTO CalendarEvents 
                      (Title, Description, EventDate, StartTime, EndTime, Location, 
                       EventType, Participants, TrainerId, IsCompleted, Notes, CreatedDate)
                      VALUES (@Title, @Description, @EventDate, @StartTime, @EndTime, @Location,
                              @EventType, @Participants, @TrainerId, @IsCompleted, @Notes, @CreatedDate)", connection))
                {
                    command.Parameters.AddWithValue("@Title", calendarEvent.Title);
                    command.Parameters.AddWithValue("@Description", calendarEvent.Description ?? "");
                    command.Parameters.AddWithValue("@EventDate", calendarEvent.EventDate);
                    command.Parameters.AddWithValue("@StartTime", calendarEvent.StartTime);
                    command.Parameters.AddWithValue("@EndTime", calendarEvent.EndTime);
                    command.Parameters.AddWithValue("@Location", calendarEvent.Location ?? "");
                    command.Parameters.AddWithValue("@EventType", calendarEvent.EventType ?? "Edzés");
                    command.Parameters.AddWithValue("@Participants", calendarEvent.Participants ?? "");
                    command.Parameters.AddWithValue("@TrainerId", calendarEvent.TrainerId);
                    command.Parameters.AddWithValue("@IsCompleted", false);
                    command.Parameters.AddWithValue("@Notes", calendarEvent.Notes ?? "");
                    command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch
            {
                return false;
            }
        }
        
        // Statisztikák
        public Statistics GetStatistics()
        {
            var stats = new Statistics();
            
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                // Edzők statisztikák
                using (var command = new SqlCommand(
                    "SELECT COUNT(*) as Total, AVG(ExperienceYears) as AvgExp, SUM(TrainedAthletes) as TotalAth FROM Trainers WHERE IsActive = 1", 
                    connection))
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        stats.TotalTrainers = Convert.ToInt32(reader["Total"]);
                        stats.AvgExperienceYears = reader["AvgExp"] != DBNull.Value ? Convert.ToDouble(reader["AvgExp"]) : 0;
                        stats.TotalAthletes = Convert.ToInt32(reader["TotalAth"]);
                    }
                }
                
                // Sportolók statisztikák
                using (var command = new SqlCommand(
                    "SELECT COUNT(*) as Total, AVG(Age) as AvgAge, COUNT(CASE WHEN IsActive = 1 THEN 1 END) as Active FROM Athletes", 
                    connection))
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        stats.TotalAthletes = Convert.ToInt32(reader["Total"]);
                        stats.AvgAthleteAge = reader["AvgAge"] != DBNull.Value ? Convert.ToDouble(reader["AvgAge"]) : 0;
                        stats.ActiveAthletes = Convert.ToInt32(reader["Active"]);
                    }
                }
                
                // Edzéstervek statisztikák
                using (var command = new SqlCommand(
                    "SELECT COUNT(*) as Total, AVG(Price) as AvgPrice, AVG(DurationWeeks) as AvgDur FROM TrainingPlans WHERE IsPublished = 1", 
                    connection))
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        stats.TotalTrainingPlans = Convert.ToInt32(reader["Total"]);
                        stats.AvgPlanPrice = reader["AvgPrice"] != DBNull.Value ? Convert.ToDecimal(reader["AvgPrice"]) : 0;
                        stats.AvgPlanDuration = reader["AvgDur"] != DBNull.Value ? Convert.ToDouble(reader["AvgDur"]) : 0;
                    }
                }
                
                // Bevétel statisztikák
                var thisMonthStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                using (var command = new SqlCommand(
                    @"SELECT SUM(Price) as TotalRev, 
                             SUM(CASE WHEN PurchaseDate >= @ThisMonth THEN Price ELSE 0 END) as MonthRev,
                             AVG(Price) as AvgPurchase
                      FROM Purchases WHERE IsPaid = 1", 
                    connection))
                {
                    command.Parameters.AddWithValue("@ThisMonth", thisMonthStart);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            stats.TotalRevenue = reader["TotalRev"] != DBNull.Value ? Convert.ToDecimal(reader["TotalRev"]) : 0;
                            stats.MonthlyRevenue = reader["MonthRev"] != DBNull.Value ? Convert.ToDecimal(reader["MonthRev"]) : 0;
                            stats.AvgPurchaseValue = reader["AvgPurchase"] != DBNull.Value ? Convert.ToDecimal(reader["AvgPurchase"]) : 0;
                        }
                    }
                }
                
                // Kategóriák eloszlása
                using (var command = new SqlCommand(
                    "SELECT Category, COUNT(*) as Count FROM TrainingPlans GROUP BY Category", 
                    connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        stats.CategoryDistribution[reader["Category"].ToString()] = Convert.ToInt32(reader["Count"]);
                    }
                }
                
                // Sporttípusok eloszlása
                using (var command = new SqlCommand(
                    "SELECT SportType, COUNT(*) as Count FROM Athletes GROUP BY SportType", 
                    connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        stats.SportTypeDistribution[reader["SportType"].ToString()] = Convert.ToInt32(reader["Count"]);
                    }
                }
                
                // Top edzők
                using (var command = new SqlCommand(
                    @"SELECT TOP 5 t.Name, t.TrainedAthletes, 
                             COUNT(tp.Id) as PlanCount, t.Rating
                      FROM Trainers t
                      LEFT JOIN TrainingPlans tp ON t.Id = tp.TrainerId
                      WHERE t.IsActive = 1
                      GROUP BY t.Id, t.Name, t.TrainedAthletes, t.Rating
                      ORDER BY t.TrainedAthletes DESC", 
                    connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        stats.TopTrainers.Add(new TopTrainer
                        {
                            Name = reader["Name"].ToString(),
                            AthleteCount = Convert.ToInt32(reader["TrainedAthletes"]),
                            PlanCount = Convert.ToInt32(reader["PlanCount"]),
                            Rating = Convert.ToDouble(reader["Rating"])
                        });
                    }
                }
                
                // Legnépszerűbb edzésterv
                using (var command = new SqlCommand(
                    @"SELECT TOP 1 Title, PurchaseCount FROM TrainingPlans 
                      ORDER BY PurchaseCount DESC", 
                    connection))
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        stats.MostSoldPlan = $"{reader["Title"]} ({reader["PurchaseCount"]} vásárlás)";
                    }
                }
                
                // Új sportolók ebben a hónapban
                using (var command = new SqlCommand(
                    "SELECT COUNT(*) FROM Athletes WHERE RegistrationDate >= @ThisMonth", 
                    connection))
                {
                    command.Parameters.AddWithValue("@ThisMonth", thisMonthStart);
                    stats.NewAthletesThisMonth = Convert.ToInt32(command.ExecuteScalar());
                }
                
                // Legnépszerűbb kategória
                var topCategory = stats.CategoryDistribution
                    .OrderByDescending(x => x.Value)
                    .FirstOrDefault();
                stats.MostPopularCategory = $"{topCategory.Key} ({topCategory.Value} terv)";
            }
            
            return stats;
        }
    }
}