using RMBCoachingApp.Models;
using System.Windows;

namespace RMBCoachingApp.Windows
{
    public partial class AthleteWindow : Window
    {
        public Athlete Athlete { get; set; }
        public bool IsEditMode { get; set; }
        public string WindowTitle => IsEditMode ? "Sportoló szerkesztése" : "Új sportoló hozzáadása";
        
        public AthleteWindow()
        {
            InitializeComponent();
            Athlete = new Athlete();
            IsEditMode = false;
            DataContext = this;
        }
        
        public AthleteWindow(Athlete athleteToEdit)
        {
            InitializeComponent();
            Athlete = athleteToEdit;
            IsEditMode = true;
            DataContext = this;
        }
        
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Validáció
            if (string.IsNullOrWhiteSpace(Athlete.Name))
            {
                MessageBox.Show("A név megadása kötelező!", "Hiba", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            if (string.IsNullOrWhiteSpace(Athlete.Email))
            {
                MessageBox.Show("Az email megadása kötelező!", "Hiba", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            if (!System.Text.RegularExpressions.Regex.IsMatch(Athlete.Email, 
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Érvénytelen email cím formátum!", "Hiba", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            if (Athlete.Age < 12 || Athlete.Age > 80)
            {
                MessageBox.Show("Az életkor 12 és 80 között lehet!", "Hiba", 
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