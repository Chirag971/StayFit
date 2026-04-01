using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using StayFitz.Models;
namespace StayFitz.Helper
{
    using System.Text.Json;

    public static class HistoryHelper
    {
        private static string key = "workout_history";

        public static List<WorkoutHistory> GetHistory()
        {
            var json = Preferences.Get(key, string.Empty);

            if (string.IsNullOrEmpty(json))
                return new List<WorkoutHistory>();

            return JsonSerializer.Deserialize<List<WorkoutHistory>>(json) ?? new List<WorkoutHistory>();
        }

        public static void SaveHistory(List<WorkoutHistory> history)
        {
            var json = JsonSerializer.Serialize(history);
            Preferences.Set(key, json);
        }

        public static void AddHistory(WorkoutHistory item)
        {
            var history = GetHistory();
            history.Insert(0, item); // latest on top
            SaveHistory(history);
        }
    }
}
