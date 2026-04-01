using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StayFitz.Models;

namespace StayFitz.Services
{
    public static class ReminderStorage
    {
        private const string KEY = "ReminderList";

        public static List<Reminder> GetReminders()
        {
            var json = Preferences.Get(KEY, string.Empty);

            if (string.IsNullOrEmpty(json))
                return new List<Reminder>();

            return JsonConvert.DeserializeObject<List<Reminder>>(json);
        }

        public static void SaveReminders(List<Reminder> reminders)
        {
            var json = JsonConvert.SerializeObject(reminders);
            Preferences.Set(KEY, json);
        }
    }
}
