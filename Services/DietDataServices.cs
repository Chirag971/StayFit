using System.Text.Json;
using StayFitz.Models;

public class DietDataServices
{
    public async Task<DietDataModel> GetDataAsync()
    {
        try
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("dietdata.json");
            using var reader = new StreamReader(stream);

            var json = await reader.ReadToEndAsync();

            System.Diagnostics.Debug.WriteLine("JSON DATA: " + json); // DEBUG

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var data = JsonSerializer.Deserialize<DietDataModel>(json, options);

            return data;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
            return null;
        }
    }
}
