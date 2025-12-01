using RMBCoachingApp.Models;
using RMBCoachingApp.Services;
using RMBCoachingApp.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Win32;

namespace RMBCoachingApp
{
    public partial class MainWindow : Window
    {
        private DatabaseService databaseService;
        private List<Trainer> trainers;
        private List<TrainingPlan> trainingPlans;
        private List<Athlete> athletes;
        private List<Purchase> purchases;
        private List<CalendarEvent> calendarEvents;
        private List<CalendarEvent> allCalendarEvents;
        private Statistics statistics;
        private DateTime currentMonth;

        public MainWindow()
        {
            InitializeComponent();
            
            // Inicializálás
            databaseService = new DatabaseService();
            trainers = new List<Trainer>();
            trainingPlans = new List<TrainingPlan>();
            athletes = new List<Athlete>();
            purchases = new List<Purchase>();
            calendarEvents = new List<CalendarEvent>();
            allCalendarEvents = new List<CalendarEvent>();
            statistics = new Statistics();
            currentMonth = DateTime.Now;
            
            // Dátum választók beállítása
            dpFromDate.SelectedDate = DateTime.Now.AddMonths(-1);
            dpToDate.SelectedDate = DateTime.Now;
            
            // Esemény típus szűrő feltöltése
            cbEventTypeFilter.Items.Clear();
            cbEventTypeFilter.Items.Add("Összes típus");
            cbEventTypeFilter.Items.Add("Edzések");
            cbEventTypeFilter.Items.Add("Találkozók");
            cbEventTypeFilter.Items.Add("Játékok");
            cbEventTypeFilter.Items.Add("Versenysorozat");
            cbEventTypeFilter.Items.Add("Edzőtábor");
            cbEventTypeFilter.SelectedIndex = 0;
            
            LoadData();
            StartAutoRefreshTimer();
        }
        
        private void StartAutoRefreshTimer()
        {
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromMinutes(5); // 5 percenként frissít
            timer.Tick += (s, e) => RefreshDashboard();
            timer.Start();
        }
        
        private void LoadData()
        {
            // Kapcsolat tesztelése
            if (databaseService.TestConnection())
            {
                tbConnectionStatus.Text = "Kapcsolat: ✅ OK";
                tbConnectionStatus.Foreground = System.Windows.Media.Brushes.Green;
                
                // Alapadatok betöltése
                RefreshTrainers();
                RefreshTrainingPlans();
                RefreshAthletes();
                RefreshPurchases();
                LoadCalendarEvents();
                LoadStatistics();
                RefreshDashboard();
                
                tbStatus.Text = "Adatok betöltve";
                tbLastUpdate.Text = $"Utolsó frissítés: {DateTime.Now:HH:mm:ss}";
            }
            else
            {
                tbConnectionStatus.Text = "Kapcsolat: ❌ HIBA";
                tbConnectionStatus.Foreground = System.Windows.Media.Brushes.Red;
                tbStatus.Text = "Nem sikerült csatlakozni az adatbázishoz";
                MessageBox.Show("Nem sikerült csatlakozni az adatbázishoz. Ellenőrizze a kapcsolat beállításokat.",
                    "Kapcsolati hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        // ==================== DASHBOARD ====================
        private void RefreshDashboard()
        {
            try
            {
                // Gyorsstatisztikák
                var stats = databaseService.GetStatistics();
                var today = DateTime.Today;
                
                // Aktív sportolók
                var activeAthletes = athletes.Count(a => a.IsActive);
                tbActiveAthletesCount.Text = activeAthletes.ToString();
                
                // Új sportolók ma
                var newAthletesToday = athletes.Count(a => a.RegistrationDate.Date == today);
                tbNewAthletesToday.Text = newAthletesToday > 0 ? $"+{newAthletesToday} ma" : "+0 ma";
                
                // Havi bevétel
                tbMonthlyRevenue.Text = $"{stats.MonthlyRevenue:N0} Ft";
                
                // Havi bevétel változás (szimulált)
                var lastMonthRevenue = stats.MonthlyRevenue * 0.9m; // 10% csökkenés feltételezése
                var changePercent = lastMonthRevenue > 0 ? 
                    (int)((stats.MonthlyRevenue - lastMonthRevenue) / lastMonthRevenue * 100) : 0;
                tbRevenueChange.Text = changePercent >= 0 ? $"↗ +{changePercent}%" : $"↘ {changePercent}%";
                tbRevenueChange.Foreground = changePercent >= 0 ? 
                    System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;
                
                // Mai események
                var todaysEvents = calendarEvents.Count(e => e.EventDate.Date == today);
                tbTodaysEvents.Text = $"{todaysEvents} esemény";
                
                // Következő esemény időpontja
                var nextEvent = calendarEvents
                    .Where(e => e.EventDate >= today && !e.IsCompleted)
                    .OrderBy(e => e.EventDate)
                    .ThenBy(e => e.StartTime)
                    .FirstOrDefault();
                tbNextEventTime.Text = nextEvent != null ? 
                    $"{nextEvent.Title} - {nextEvent.StartTime:hh\\:mm}" : "Nincs esemény";
                
                // Aktív edzéstervek
                var activePlans = trainingPlans.Count(p => p.IsPublished);
                tbActivePlans.Text = activePlans.ToString();
                
                // Legnépszerűbb edzésterv
                var mostPopularPlan = trainingPlans
                    .OrderByDescending(p => p.PurchaseCount)
                    .FirstOrDefault();
                tbMostPopularPlan.Text = mostPopularPlan != null ? 
                    $"{mostPopularPlan.Title} ({mostPopularPlan.PurchaseCount})" : "Nincs adat";
                
                // Legutóbbi vásárlások
                var recentPurchases = purchases
                    .OrderByDescending(p => p.PurchaseDate)
                    .Take(10)
                    .ToList();
                dgRecentPurchases.ItemsSource = recentPurchases;
                
                // Közelgő események (7 nap)
                var upcomingEvents = calendarEvents
                    .Where(e => e.EventDate >= today && e.EventDate <= today.AddDays(7) && !e.IsCompleted)
                    .OrderBy(e => e.EventDate)
                    .ThenBy(e => e.StartTime)
                    .Take(10)
                    .ToList();
                dgUpcomingEvents.ItemsSource = upcomingEvents;
                
                tbLastUpdate.Text = $"Utolsó frissítés: {DateTime.Now:HH:mm:ss}";
            }
            catch (Exception ex)
            {
                tbStatus.Text = $"Hiba a dashboard frissítésekor: {ex.Message}";
            }
        }
        
        // ==================== EDZŐK ====================
        private void RefreshTrainers()
        {
            trainers = databaseService.GetTrainers();
            ApplyTrainerFilters();
            tbStatus.Text = $"Edzők betöltve: {trainers.Count}";
        }
        
        private void ApplyTrainerFilters()
        {
            var filteredTrainers = trainers.AsEnumerable();
            
            // Keresés
            if (!string.IsNullOrWhiteSpace(txtSearchTrainer.Text))
            {
                var search = txtSearchTrainer.Text.ToLower();
                filteredTrainers = filteredTrainers.Where(t =>
                    t.Name.ToLower().Contains(search) ||
                    t.Specialty.ToLower().Contains(search) ||
                    (t.Email?.ToLower().Contains(search) ?? false));
            }
            
            // Csak aktívak
            if (cbActiveTrainersOnly.IsChecked == true)
            {
                filteredTrainers = filteredTrainers.Where(t => t.IsActive);
            }
            
            dgTrainers.ItemsSource = filteredTrainers.ToList();
        }
        
        private void BtnRefreshTrainers_Click(object sender, RoutedEventArgs e)
        {
            RefreshTrainers();
            LoadStatistics();
            tbStatus.Text = "Edzők lista frissítve";
        }
        
        private void BtnAddTrainer_Click(object sender, RoutedEventArgs e)
        {
            var window = new TrainerWindow();
            if (window.ShowDialog() == true)
            {
                if (databaseService.AddTrainer(window.Trainer))
                {
                    RefreshTrainers();
                    LoadStatistics();
                    RefreshDashboard();
                    tbStatus.Text = "Új edző sikeresen hozzáadva";
                }
                else
                {
                    MessageBox.Show("Nem sikerült hozzáadni az edzőt!", "Hiba", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        
        private void BtnEditTrainer_Click(object sender, RoutedEventArgs e)
        {
            if (dgTrainers.SelectedItem is Trainer selectedTrainer)
            {
                var window = new TrainerWindow(selectedTrainer);
                if (window.ShowDialog() == true)
                {
                    if (databaseService.UpdateTrainer(window.Trainer))
                    {
                        RefreshTrainers();
                        LoadStatistics();
                        RefreshDashboard();
                        tbStatus.Text = "Edző sikeresen frissítve";
                    }
                    else
                    {
                        MessageBox.Show("Nem sikerült frissíteni az edzőt!", "Hiba", 
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Kérem válasszon ki egy edzőt a szerkesztéshez!", "Figyelmeztetés",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        
        private void BtnDeleteTrainer_Click(object sender, RoutedEventArgs e)
        {
            if (dgTrainers.SelectedItem is Trainer selectedTrainer)
            {
                var result = MessageBox.Show($"Biztosan törölni szeretné a(z) '{selectedTrainer.Name}' edzőt?\n\nFigyelem: A hozzá tartozó edzéstervek is törlődhetnek!",
                    "Megerősítés", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (databaseService.DeleteTrainer(selectedTrainer.Id))
                    {
                        RefreshTrainers();
                        LoadStatistics();
                        RefreshDashboard();
                        tbStatus.Text = "Edző sikeresen törölve";
                    }
                    else
                    {
                        MessageBox.Show("Nem sikerült törölni az edzőt!", "Hiba", 
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Kérem válasszon ki egy edzőt a törléshez!", "Figyelmeztetés",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        
        private void BtnExportTrainers_Click(object sender, RoutedEventArgs e)
        {
            var saveDialog = new SaveFileDialog
            {
                Filter = "CSV fájl (*.csv)|*.csv",
                FileName = $"edzok_export_{DateTime.Now:yyyyMMdd_HHmm}.csv"
            };
            
            if (saveDialog.ShowDialog() == true)
            {
                ExportService.ExportToCsv(trainers, saveDialog.FileName);
                tbStatus.Text = "Edzők exportálva CSV fájlba";
            }
        }
        
        private void TxtSearchTrainer_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyTrainerFilters();
        }
        
        private void CbActiveTrainersOnly_Checked(object sender, RoutedEventArgs e)
        {
            ApplyTrainerFilters();
        }
        
        // ==================== EDZÉSTERVEK ====================
        private void RefreshTrainingPlans()
        {
            trainingPlans = databaseService.GetTrainingPlans();
            ApplyPlanFilters();
            tbStatus.Text = $"Edzéstervek betöltve: {trainingPlans.Count}";
        }
        
        private void ApplyPlanFilters()
        {
            var filteredPlans = trainingPlans.AsEnumerable();
            
            // Kategória szűrés
            if (cbPlanCategoryFilter.SelectedIndex > 0 && cbPlanCategoryFilter.SelectedItem is ComboBoxItem selectedItem)
            {
                var category = selectedItem.Content.ToString();
                filteredPlans = filteredPlans.Where(p => p.Category == category);
            }
            
            // Csak publikált
            if (cbPublishedPlansOnly.IsChecked == true)
            {
                filteredPlans = filteredPlans.Where(p => p.IsPublished);
            }
            
            dgTrainingPlans.ItemsSource = filteredPlans.ToList();
        }
        
        private void BtnRefreshPlans_Click(object sender, RoutedEventArgs e)
        {
            RefreshTrainingPlans();
            LoadStatistics();
            tbStatus.Text = "Edzéstervek lista frissítve";
        }
        
        private void BtnAddPlan_Click(object sender, RoutedEventArgs e)
        {
            if (trainers.Count == 0)
            {
                MessageBox.Show("Először adjon hozzá edzőket!", "Figyelmeztetés",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            var window = new TrainingPlanWindow(trainers);
            if (window.ShowDialog() == true)
            {
                if (databaseService.AddTrainingPlan(window.TrainingPlan))
                {
                    RefreshTrainingPlans();
                    LoadStatistics();
                    RefreshDashboard();
                    tbStatus.Text = "Új edzésterv sikeresen hozzáadva";
                }
                else
                {
                    MessageBox.Show("Nem sikerült hozzáadni az edzéstervet!", "Hiba", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        
        private void BtnEditPlan_Click(object sender, RoutedEventArgs e)
        {
            if (dgTrainingPlans.SelectedItem is TrainingPlan selectedPlan)
            {
                var window = new TrainingPlanWindow(trainers, selectedPlan);
                if (window.ShowDialog() == true)
                {
                    if (databaseService.UpdateTrainingPlan(window.TrainingPlan))
                    {
                        RefreshTrainingPlans();
                        LoadStatistics();
                        RefreshDashboard();
                        tbStatus.Text = "Edzésterv sikeresen frissítve";
                    }
                    else
                    {
                        MessageBox.Show("Nem sikerült frissíteni az edzéstervet!", "Hiba", 
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Kérem válasszon ki egy edzéstervet a szerkesztéshez!", "Figyelmeztetés",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        
        private void BtnDeletePlan_Click(object sender, RoutedEventArgs e)
        {
            if (dgTrainingPlans.SelectedItem is TrainingPlan selectedPlan)
            {
                var result = MessageBox.Show($"Biztosan törölni szeretné a(z) '{selectedPlan.Title}' edzéstervet?",
                    "Megerősítés", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (databaseService.DeleteTrainingPlan(selectedPlan.Id))
                    {
                        RefreshTrainingPlans();
                        LoadStatistics();
                        RefreshDashboard();
                        tbStatus.Text = "Edzésterv sikeresen törölve";
                    }
                    else
                    {
                        MessageBox.Show("Nem sikerült törölni az edzéstervet!", "Hiba", 
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Kérem válasszon ki egy edzéstervet a törléshez!", "Figyelmeztetés",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        
        private void BtnExportPlans_Click(object sender, RoutedEventArgs e)
        {
            var saveDialog = new SaveFileDialog
            {
                Filter = "CSV fájl (*.csv)|*.csv",
                FileName = $"edzestervek_export_{DateTime.Now:yyyyMMdd_HHmm}.csv"
            };
            
            if (saveDialog.ShowDialog() == true)
            {
                ExportService.ExportToCsv(trainingPlans, saveDialog.FileName);
                tbStatus.Text = "Edzéstervek exportálva CSV fájlba";
            }
        }
        
        private void CbPlanCategoryFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyPlanFilters();
        }
        
        private void CbPublishedPlansOnly_Checked(object sender, RoutedEventArgs e)
        {
            ApplyPlanFilters();
        }
        
        // ==================== SPORTOLÓK ====================
        private void RefreshAthletes()
        {
            athletes = databaseService.GetAthletes();
            ApplyAthleteFilters();
            tbStatus.Text = $"Sportolók betöltve: {athletes.Count}";
        }
        
        private void ApplyAthleteFilters()
        {
            var filteredAthletes = athletes.AsEnumerable();
            
            // Keresés
            if (!string.IsNullOrWhiteSpace(txtSearchAthlete.Text))
            {
                var search = txtSearchAthlete.Text.ToLower();
                filteredAthletes = filteredAthletes.Where(a =>
                    a.Name.ToLower().Contains(search) ||
                    a.Email.ToLower().Contains(search) ||
                    (a.Phone?.ToLower().Contains(search) ?? false));
            }
            
            // Sporttípus szűrés
            if (cbAthleteSportFilter.SelectedIndex > 0 && cbAthleteSportFilter.SelectedItem is ComboBoxItem selectedItem)
            {
                var sportType = selectedItem.Content.ToString();
                filteredAthletes = filteredAthletes.Where(a => a.SportType == sportType);
            }
            
            // Csak aktívak
            if (cbActiveAthletesOnly.IsChecked == true)
            {
                filteredAthletes = filteredAthletes.Where(a => a.IsActive);
            }
            
            dgAthletes.ItemsSource = filteredAthletes.ToList();
        }
        
        private void BtnRefreshAthletes_Click(object sender, RoutedEventArgs e)
        {
            RefreshAthletes();
            LoadStatistics();
            tbStatus.Text = "Sportolók lista frissítve";
        }
        
        private void BtnAddAthlete_Click(object sender, RoutedEventArgs e)
        {
            var window = new AthleteWindow();
            if (window.ShowDialog() == true)
            {
                if (databaseService.AddAthlete(window.Athlete))
                {
                    RefreshAthletes();
                    LoadStatistics();
                    RefreshDashboard();
                    tbStatus.Text = "Új sportoló sikeresen hozzáadva";
                }
                else
                {
                    MessageBox.Show("Nem sikerült hozzáadni a sportolót!", "Hiba", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        
        private void BtnEditAthlete_Click(object sender, RoutedEventArgs e)
        {
            if (dgAthletes.SelectedItem is Athlete selectedAthlete)
            {
                var window = new AthleteWindow(selectedAthlete);
                if (window.ShowDialog() == true)
                {
                    if (databaseService.UpdateAthlete(window.Athlete))
                    {
                        RefreshAthletes();
                        LoadStatistics();
                        RefreshDashboard();
                        tbStatus.Text = "Sportoló sikeresen frissítve";
                    }
                    else
                    {
                        MessageBox.Show("Nem sikerült frissíteni a sportolót!", "Hiba", 
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Kérem válasszon ki egy sportolót a szerkesztéshez!", "Figyelmeztetés",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        
        private void BtnDeleteAthlete_Click(object sender, RoutedEventArgs e)
        {
            if (dgAthletes.SelectedItem is Athlete selectedAthlete)
            {
                var result = MessageBox.Show($"Biztosan törölni szeretné a(z) '{selectedAthlete.Name}' sportolót?",
                    "Megerősítés", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (databaseService.DeleteAthlete(selectedAthlete.Id))
                    {
                        RefreshAthletes();
                        LoadStatistics();
                        RefreshDashboard();
                        tbStatus.Text = "Sportoló sikeresen törölve";
                    }
                    else
                    {
                        MessageBox.Show("Nem sikerült törölni a sportolót!", "Hiba", 
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Kérem válasszon ki egy sportolót a törléshez!", "Figyelmeztetés",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        
        private void BtnExportAthletes_Click(object sender, RoutedEventArgs e)
        {
            var saveDialog = new SaveFileDialog
            {
                Filter = "CSV fájl (*.csv)|*.csv",
                FileName = $"sportolok_export_{DateTime.Now:yyyyMMdd_HHmm}.csv"
            };
            
            if (saveDialog.ShowDialog() == true)
            {
                ExportService.ExportToCsv(athletes, saveDialog.FileName);
                tbStatus.Text = "Sportolók exportálva CSV fájlba";
            }
        }
        
        private void TxtSearchAthlete_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyAthleteFilters();
        }
        
        private void CbAthleteSportFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyAthleteFilters();
        }
        
        private void CbActiveAthletesOnly_Checked(object sender, RoutedEventArgs e)
        {
            ApplyAthleteFilters();
        }
        
        // ==================== VÁSÁRLÁSOK ====================
        private void RefreshPurchases()
        {
            DateTime? fromDate = dpFromDate.SelectedDate;
            DateTime? toDate = dpToDate.SelectedDate;
            
            purchases = databaseService.GetPurchases(fromDate, toDate);
            dgPurchases.ItemsSource = purchases;
            
            UpdateRevenueStats();
            tbStatus.Text = $"Vásárlások betöltve: {purchases.Count}";
        }
        
        private void BtnRefreshPurchases_Click(object sender, RoutedEventArgs e)
        {
            RefreshPurchases();
            tbStatus.Text = "Vásárlások lista frissítve";
        }
        
        private void BtnFilterPurchases_Click(object sender, RoutedEventArgs e)
        {
            RefreshPurchases();
        }
        
        private void UpdateRevenueStats()
        {
            var totalRevenue = purchases.Where(p => p.IsPaid).Sum(p => p.Price);
            var monthlyRevenue = purchases
                .Where(p => p.IsPaid && p.PurchaseDate.Month == DateTime.Now.Month)
                .Sum(p => p.Price);
            
            tbTotalRevenue.Text = $"Teljes bevétel: {totalRevenue:N0} Ft | Havi: {monthlyRevenue:N0} Ft";
        }
        
        private void BtnExportSales_Click(object sender, RoutedEventArgs e)
        {
            var saveDialog = new SaveFileDialog
            {
                Filter = "Excel fájl (*.xlsx)|*.xlsx",
                FileName = $"bevetelek_export_{DateTime.Now:yyyyMMdd}.xlsx"
            };
            
            if (saveDialog.ShowDialog() == true)
            {
                ExportService.ExportSalesToExcel(purchases, saveDialog.FileName);
                tbStatus.Text = "Bevételek exportálva Excel fájlba";
            }
        }
        
        // ==================== NAPTÁR ====================
        private void LoadCalendarEvents()
        {
            allCalendarEvents = databaseService.GetCalendarEvents(currentMonth);
            ApplyCalendarFilters();
            tbCurrentMonth.Text = currentMonth.ToString("yyyy. MMMM");
            tbStatus.Text = $"Naptár események betöltve: {allCalendarEvents.Count}";
        }
        
        private void ApplyCalendarFilters()
        {
            var filteredEvents = allCalendarEvents.AsEnumerable();
            
            // Esemény típus szűrés
            if (cbEventTypeFilter.SelectedIndex > 0 && cbEventTypeFilter.SelectedItem is ComboBoxItem selectedItem)
            {
                var eventType = selectedItem.Content.ToString().Replace("ések", "és"); // Edzések -> Edzés
                filteredEvents = filteredEvents.Where(e => e.EventType == eventType);
            }
            
            // Csak közelgő események
            if (cbUpcomingEventsOnly.IsChecked == true)
            {
                filteredEvents = filteredEvents.Where(e => e.EventDate >= DateTime.Today || e.IsCompleted == false);
            }
            
            dgCalendar.ItemsSource = filteredEvents.OrderBy(e => e.EventDate).ThenBy(e => e.StartTime).ToList();
        }
        
        private void BtnPrevMonth_Click(object sender, RoutedEventArgs e)
        {
            currentMonth = currentMonth.AddMonths(-1);
            LoadCalendarEvents();
        }
        
        private void BtnNextMonth_Click(object sender, RoutedEventArgs e)
        {
            currentMonth = currentMonth.AddMonths(1);
            LoadCalendarEvents();
        }
        
        private void BtnToday_Click(object sender, RoutedEventArgs e)
        {
            currentMonth = DateTime.Now;
            LoadCalendarEvents();
        }
        
        private void BtnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            var window = new CalendarEventWindow(trainers);
            if (window.ShowDialog() == true)
            {
                if (databaseService.AddCalendarEvent(window.CalendarEvent))
                {
                    LoadCalendarEvents();
                    RefreshDashboard();
                    tbStatus.Text = "Új esemény hozzáadva";
                }
                else
                {
                    MessageBox.Show("Nem sikerült hozzáadni az eseményt!", "Hiba", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        
        private void CbUpcomingEventsOnly_Checked(object sender, RoutedEventArgs e)
        {
            ApplyCalendarFilters();
        }
        
        private void CbEventTypeFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyCalendarFilters();
        }
        
        // ==================== STATISZTIKÁK ====================
        private void LoadStatistics()
        {
            statistics = databaseService.GetStatistics();
            
            // Edzők statisztikák
            tbTotalTrainers.Text = $"Összes edző: {trainers.Count}";
            tbAvgExperience.Text = $"Átlagos tapasztalat: {trainers.Average(t => t.ExperienceYears):F1} év";
            tbActiveTrainers.Text = $"Aktív edzők: {trainers.Count(t => t.IsActive)}";
            
            var topTrainer = trainers.OrderByDescending(t => t.TrainedAthletes).FirstOrDefault();
            tbTopTrainer.Text = topTrainer != null ? 
                $"Legtapasztaltabb: {topTrainer.Name} ({topTrainer.TrainedAthletes} sportoló)" : "Nincs adat";
            
            // Sportolók statisztikák
            tbTotalAthletes.Text = $"Összes sportoló: {athletes.Count}";
            tbActiveAthletes.Text = $"Aktív sportolók: {athletes.Count(a => a.IsActive)}";
            tbAvgAthleteAge.Text = $"Átlagéletkor: {athletes.Average(a => a.Age):F1} év";
            tbNewAthletesThisMonth.Text = $"Új sportolók ebben a hónapban: {statistics.NewAthletesThisMonth}";
            
            // Edzéstervek statisztikák
            tbTotalPlans.Text = $"Összes edzésterv: {trainingPlans.Count}";
            tbAvgPrice.Text = $"Átlagos ár: {trainingPlans.Average(p => p.Price):F0} Ft";
            tbAvgDuration.Text = $"Átlagos időtartam: {trainingPlans.Average(p => p.DurationWeeks):F1} hét";
            tbMostPopularCategory.Text = $"Legnépszerűbb kategória: {statistics.MostPopularCategory}";
            
            // Pénzügyi statisztikák
            tbTotalRevenue.Text = $"Teljes bevétel: {statistics.TotalRevenue:N0} Ft";
            tbMonthlyRevenue.Text = $"Havi bevétel: {statistics.MonthlyRevenue:N0} Ft";
            tbAvgPurchaseValue.Text = $"Átlagos vásárlás: {statistics.AvgPurchaseValue:N0} Ft";
            tbMostSoldPlan.Text = $"Legkelendőbb terv: {statistics.MostSoldPlan}";
            
            // Kategóriák eloszlása
            icCategories.ItemsSource = statistics.CategoryDistribution
                .Select(c => new TextBlock 
                { 
                    Text = $"• {c.Key}: {c.Value} terv ({Math.Round(c.Value * 100.0 / statistics.TotalTrainingPlans)}%)",
                    Margin = new Thickness(0, 2, 0, 2),
                    FontSize = 13
                });
            
            // Sporttípusok eloszlása
            icSportTypes.ItemsSource = statistics.SportTypeDistribution
                .Select(c => new TextBlock 
                { 
                    Text = $"• {c.Key}: {c.Value} sportoló",
                    Margin = new Thickness(0, 2, 0, 2),
                    FontSize = 13
                });
            
            // Top edzők
            dgTopTrainers.ItemsSource = statistics.TopTrainers;
            
            tbStatus.Text = "Statisztikák frissítve";
        }
        
        // ==================== ÁLTALÁNOS ====================
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Dashboard automatikus megjelenítése
            var tabControl = this.FindName("tabControl") as TabControl;
            if (tabControl != null && tabControl.Items.Count > 0)
            {
                tabControl.SelectedIndex = 0;
            }
        }
    }
}