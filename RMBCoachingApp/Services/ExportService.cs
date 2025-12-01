using RMBCoachingApp.Models;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Windows;

namespace RMBCoachingApp.Services
{
    public static class ExportService
    {
        public static void ExportToCsv<T>(List<T> data, string filePath)
        {
            try
            {
                var sb = new StringBuilder();
                
                // Fejléc
                var properties = typeof(T).GetProperties();
                var header = string.Join(",", properties.Select(p => p.Name));
                sb.AppendLine(header);
                
                // Adatok
                foreach (var item in data)
                {
                    var values = properties.Select(p => 
                    {
                        var value = p.GetValue(item);
                        var stringValue = value?.ToString() ?? "";
                        
                        // CSV speciális karakterek kezelése
                        if (stringValue.Contains(",") || stringValue.Contains("\"") || stringValue.Contains("\n"))
                        {
                            stringValue = $"\"{stringValue.Replace("\"", "\"\"")}\"";
                        }
                        
                        return stringValue;
                    });
                    
                    sb.AppendLine(string.Join(",", values));
                }
                
                File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba az exportálás közben: {ex.Message}", "Hiba", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        public static void ExportSalesToExcel(List<Purchase> purchases, string filePath)
        {
            try
            {
                var sb = new StringBuilder();
                
                // Fejléc
                sb.AppendLine("Vásárlás ID,Sportoló,Edzésterv,Dátum,Ár (Ft),Fizetési mód,Fizetve,Számlaszám,Megjegyzés");
                
                // Adatok
                foreach (var purchase in purchases)
                {
                    sb.AppendLine(
                        $"{purchase.Id}," +
                        $"\"{purchase.AthleteName}\"," +
                        $"\"{purchase.TrainingPlanTitle}\"," +
                        $"{purchase.PurchaseDate:yyyy.MM.dd HH:mm}," +
                        $"{purchase.Price}," +
                        $"{purchase.PaymentMethod}," +
                        $"{(purchase.IsPaid ? "Igen" : "Nem")}," +
                        $"{purchase.InvoiceNumber}," +
                        $"\"{purchase.Notes}\"");
                }
                
                File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba az exportálás közben: {ex.Message}", "Hiba", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}