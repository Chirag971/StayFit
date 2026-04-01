using System.Text.Json;
using StayFitz.Models;

namespace StayFitz.Services
{
    public static class HistoryService
    {
        static string filePath = Path.Combine(FileSystem.AppDataDirectory, "history.json");

        // ADD NEW HISTORY
        public static async Task SaveHistory(WorkoutHistory historyItem)
        {
            var list = await GetHistory();

            list.Insert(0, historyItem);

            await SaveAllHistory(list);
        }

        // GET ALL HISTORY
        public static async Task<List<WorkoutHistory>> GetHistory()
        {
            if (!File.Exists(filePath))
                return new List<WorkoutHistory>();

            var json = await File.ReadAllTextAsync(filePath);

            return JsonSerializer.Deserialize<List<WorkoutHistory>>(json)
                   ?? new List<WorkoutHistory>();
        }

        // SAVE FULL LIST
        public static async Task SaveAllHistory(List<WorkoutHistory> historyList)
        {
            var json = JsonSerializer.Serialize(historyList);
            await File.WriteAllTextAsync(filePath, json);
        }

        // DELETE SINGLE HISTORY
        public static async Task DeleteHistory(WorkoutHistory item)
        {
            var history = await GetHistory();

            var match = history.FirstOrDefault(x =>
                x.Date == item.Date &&
                x.Mode == item.Mode &&
                x.Duration == item.Duration);

            if (match != null)
            {
                history.Remove(match);
                await SaveAllHistory(history);
            }
        }

        // CLEAR ALL HISTORY 
        public static async Task ClearAll()
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }


    //public static class HistoryService
    //{
    //    // ✅ NEVER NULL
    //    private static List<WorkoutHistory> _historyList = new List<WorkoutHistory>();

    //    public static void SaveHistory(WorkoutHistory history)
    //    {
    //        _historyList.Insert(0, history);
    //    }

    //    public static List<WorkoutHistory> GetHistory()
    //    {
    //        return _historyList ?? new List<WorkoutHistory>(); // ✅ safety
    //    }

    //    public static void DeleteHistory(WorkoutHistory history)
    //    {
    //        if (_historyList.Contains(history))
    //        {
    //            _historyList.Remove(history);
    //        }
    //    }
    //}
   
}
