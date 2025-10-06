using RMBCoachingApp.Models;
using RMBCoachingApp.Services;
using RMBCoachingApp.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Controls;

namespace RMBCoachingApp
{
    public partial class MainWindow : Window
    {
        private DatabaseService databaseService;
        private List<Trainer> trainers;
        private List<TrainingPlan> trainingPlans;

        public MainWindow()
        {
            InitializeComponent();
            databaseService = new DatabaseService();
            LoadData();
        }

        private void LoadData()
        {
            // Kapcsolat tesztelése
            if (databaseService.TestConnection())
            {
                tbConnectionStatus.Text = "Kapcsolat: OK";
                RefreshTrainers();
                RefreshTrainingPlans();
                LoadStatistics();
            }
            else
            {
                tbConnectionStatus.Text = "Kapcsolat: HIBA";
            }
        }

        private void RefreshTrainers()
        {
            trainers = databaseService.GetTrainers();
            dgTrainers.ItemsSource = trainers;
            tbStatus.Text = $"Edzők betöltve: {trainers.Count}";
        }

        private void RefreshTrainingPlans()
        {
            trainingPlans = databaseService.GetTrainingPlans();
            dgTrainingPlans.ItemsSource = trainingPlans;
            tbStatus.Text = $"Edzéstervek betöltve: {trainingPlans.Count}";
        }

        private void LoadStatistics()
        {
            // Edző statisztikák
            tbTotalTrainers.Text = $"Összes edző: {trainers.Count}";
            tbAvgExperience.Text = $"Átlagos tapasztalat: {trainers.Average(t => t.ExperienceYears):F1} év";
            tbTotalAthletes.Text = $"Összes képzett sportoló: {trainers.Sum(t => t.TrainedAthletes)}";

            // Edzéstervek statisztikák
            tbTotalPlans.Text = $"Összes edzésterv: {trainingPlans.Count}";
            tbAvgPrice.Text = $"Átlagos ár: {trainingPlans.Average(p => p.Price):F0} Ft";
            tbAvgDuration.Text = $"Átlagos időtartam: {trainingPlans.Average(p => p.DurationWeeks):F1} hét";

            // Kategóriák
            var categories = trainingPlans.GroupBy(p => p.Category)
                                         .Select(g => new { Category = g.Key, Count = g.Count() });

            icCategories.ItemsSource = categories.Select(c =>
                new TextBlock { Text = $"{c.Category}: {c.Count} terv", Margin = new Thickness(0, 2, 0, 2) });
        }

        // Edző gombok
        private void BtnRefreshTrainers_Click(object sender, RoutedEventArgs e)
        {
            RefreshTrainers();
            LoadStatistics();
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
                    tbStatus.Text = "Új edző sikeresen hozzáadva";
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
                        tbStatus.Text = "Edző sikeresen frissítve";
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
                var result = MessageBox.Show($"Biztosan törölni szeretné a(z) '{selectedTrainer.Name}' edzőt?",
                    "Megerősítés", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (databaseService.DeleteTrainer(selectedTrainer.Id))
                    {
                        RefreshTrainers();
                        LoadStatistics();
                        tbStatus.Text = "Edző sikeresen törölve";
                    }
                }
            }
            else
            {
                MessageBox.Show("Kérem válasszon ki egy edzőt a törléshez!", "Figyelmeztetés",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Edzéstervek gombok
        private void BtnRefreshPlans_Click(object sender, RoutedEventArgs e)
        {
            RefreshTrainingPlans();
            LoadStatistics();
        }

        private void BtnAddPlan_Click(object sender, RoutedEventArgs e)
        {
            var window = new TrainingPlanWindow(trainers);
            if (window.ShowDialog() == true)
            {
                if (databaseService.AddTrainingPlan(window.TrainingPlan))
                {
                    RefreshTrainingPlans();
                    LoadStatistics();
                    tbStatus.Text = "Új edzésterv sikeresen hozzáadva";
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
                        tbStatus.Text = "Edzésterv sikeresen frissítve";
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
                        tbStatus.Text = "Edzésterv sikeresen törölve";
                    }
                }
            }
            else
            {
                MessageBox.Show("Kérem válasszon ki egy edzéstervet a törléshez!", "Figyelmeztetés",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}