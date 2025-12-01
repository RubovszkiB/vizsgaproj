using RMBCoachingApp.Models;
using System;
using System.Collections.Generic;
using System.Windows;

namespace RMBCoachingApp.Windows
{
    public partial class CalendarEventWindow : Window
    {
        public CalendarEvent CalendarEvent { get; set; }
        public List<Trainer> Trainers { get; set; }
        
        public CalendarEventWindow()
        {
            InitializeComponent();
            CalendarEvent = new CalendarEvent
            {
                EventDate = DateTime.Today,
                StartTime = new TimeSpan(10, 0, 0),
                EndTime = new TimeSpan(11, 0, 0),
                EventType = "Edzés"
            };
            Trainers = new List<Trainer>();
            DataContext = this;
        }
        
        public CalendarEventWindow(List<Trainer> trainers)
        {
            InitializeComponent();
            CalendarEvent = new CalendarEvent
            {
                EventDate = DateTime.Today,
                StartTime = new TimeSpan(10, 0, 0),
                EndTime = new TimeSpan(11, 0, 0),
                EventType = "Edzés"
            };
            Trainers = trainers;
            DataContext = this;
        }
        
        public CalendarEventWindow(List<Trainer> trainers, CalendarEvent eventToEdit)
        {
            InitializeComponent();
            CalendarEvent = eventToEdit;
            Trainers = trainers;
            DataContext = this;
        }
        
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Validáció
            if (string.IsNullOrWhiteSpace(CalendarEvent.Title))
            {
                MessageBox.Show("Az esemény nevének megadása kötelező!", "Hiba", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            if (!dpEventDate.SelectedDate.HasValue)
            {
                MessageBox.Show("A dátum megadása kötelező!", "Hiba", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            if (CalendarEvent.EventDate < DateTime.Today.AddDays(-1))
            {
                MessageBox.Show("A dátum nem lehet múltbeli!", "Hiba", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            // Idő validálása
            if (CalendarEvent.EndTime <= CalendarEvent.StartTime)
            {
                MessageBox.Show("A befejezési időnek későbbinek kell lennie, mint a kezdési idő!", "Hiba", 
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