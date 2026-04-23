using System.Text.Json;
using StayFitz.Models;

namespace StayFitz.Services
{
    public class DataService
    {
        public async Task<HealthData> GetHealthDataAsync()
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("articles.json");
                using var reader = new StreamReader(stream);
                var json = await reader.ReadToEndAsync();

                return JsonSerializer.Deserialize<HealthData>(json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"JSON ERROR: {ex.Message}");
                return null;
            }
        }
    }
}