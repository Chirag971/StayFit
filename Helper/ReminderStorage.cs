using System.Text.Json;
using StayFitz.Models;

public static class ReminderStorage
{
    private static string filePath =
        Path.Combine(FileSystem.AppDataDirectory, "reminders.json");

    public static void SaveReminders(List<Reminder> reminders)
    {
        var json = JsonSerializer.Serialize(reminders);
        File.WriteAllText(filePath, json);
    }

    public static List<Reminder> GetReminders()
    {
        if (!File.Exists(filePath))
            return new List<Reminder>();

        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<Reminder>>(json);
    }
}