using System.Text.Json;
using StayFitz.Models;

namespace StayFitz.Services
{
    public static class ExerciseService
    {
        public static async Task<List<Exercise>> GetExercisesAsync()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("exercises.json");
            using var reader = new StreamReader(stream);

            var json = await reader.ReadToEndAsync();

            return JsonSerializer.Deserialize<List<Exercise>>(json);
        }
    }
}
