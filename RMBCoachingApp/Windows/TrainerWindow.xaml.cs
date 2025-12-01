using RMBCoachingApp.Models;
using System.Windows;

namespace RMBCoachingApp.Windows
{
    public partial class TrainerWindow : Window
    {
        public Trainer Trainer { get; set; }
        public bool IsEditMode { get; set; }
        public string WindowTitle => IsEditMode ? "Edző szerkesztése" : "Új edző hozzáadása";
        
        public TrainerWindow()
        {
            InitializeComponent();
            Trainer = new Trainer();
            IsEditMode = false;
            DataContext = this;
        }
        
        public TrainerWindow(Trainer trainerToEdit)
        {
            InitializeComponent();
            Trainer = trainerToEdit;
            IsEditMode = true;
            DataContext = this;
        }
        
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Validáció
            if (string.IsNullOrWhiteSpace(Trainer.Name))
            {
                MessageBox.Show("A név megadása kötelező!", "Hiba", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            if (string.IsNullOrWhiteSpace(Trainer.Specialty))
            {
                MessageBox.Show("A szakértelem megadása kötelező!", "Hiba", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            if (Trainer.ExperienceYears < 0 || Trainer.ExperienceYears > 50)
            {
                MessageBox.Show("A tapasztalat 0 és 50 év között lehet!", "Hiba", 
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