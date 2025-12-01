using RMBCoachingApp.Models;
using System.Collections.Generic;
using System.Windows;

namespace RMBCoachingApp.Windows
{
    public partial class TrainingPlanWindow : Window
    {
        public TrainingPlan TrainingPlan { get; set; }
        public List<Trainer> Trainers { get; set; }
        public bool IsEditMode { get; set; }
        public string WindowTitle => IsEditMode ? "Edzésterv szerkesztése" : "Új edzésterv hozzáadása";
        
        public TrainingPlanWindow(List<Trainer> trainers)
        {
            InitializeComponent();
            Trainers = trainers;
            TrainingPlan = new TrainingPlan();
            IsEditMode = false;
            DataContext = this;
        }
        
        public TrainingPlanWindow(List<Trainer> trainers, TrainingPlan planToEdit)
        {
            InitializeComponent();
            Trainers = trainers;
            TrainingPlan = planToEdit;
            IsEditMode = true;
            DataContext = this;
        }
        
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Validáció
            if (string.IsNullOrWhiteSpace(TrainingPlan.Title))
            {
                MessageBox.Show("A cím megadása kötelező!", "Hiba", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            if (string.IsNullOrWhiteSpace(TrainingPlan.Category))
            {
                MessageBox.Show("A kategória megadása kötelező!", "Hiba", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            if (TrainingPlan.DurationWeeks < 1 || TrainingPlan.DurationWeeks > 52)
            {
                MessageBox.Show("Az időtartam 1 és 52 hét között lehet!", "Hiba", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            if (TrainingPlan.Price < 0 || TrainingPlan.Price > 1000000)
            {
                MessageBox.Show("Az ár 0 és 1,000,000 Ft között lehet!", "Hiba", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            if (TrainingPlan.TrainerId == 0)
            {
                MessageBox.Show("Válasszon edzőt!", "Hiba", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            DialogResult = true;
            Close();
        }
        
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}