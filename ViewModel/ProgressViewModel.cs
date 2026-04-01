using CommunityToolkit.Mvvm.ComponentModel;
using Microcharts;
using SkiaSharp;
using StayFitz.Services;

public partial class ProgressViewModel : ObservableObject
{
    [ObservableProperty]
    int totalWorkouts;

    [ObservableProperty]
    int totalTime;

    [ObservableProperty]
    int totalExercises;

    [ObservableProperty]
    Chart weeklyChart;

    public async Task LoadProgress()
    {
        var historyList = await HistoryService.GetHistory();

        // ✅ BASIC STATS
        TotalWorkouts = historyList.Count;
        TotalTime = historyList.Sum(x => x.TotalTime);
        TotalExercises = historyList.Sum(x => x.TotalExercises);

        // ✅ WEEKLY GRAPH LOGIC
        var weekData = new int[7];

        foreach (var item in historyList)
        {
            int dayIndex = (int)item.Date.DayOfWeek;
            weekData[dayIndex]++;
        }

        string[] days = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };

        var entries = new List<ChartEntry>();

        for (int i = 0; i < 7; i++)
        {
            entries.Add(new ChartEntry(weekData[i])
            {
                Label = days[i],
                ValueLabel = weekData[i].ToString(),
                Color = SKColor.Parse("#35E0A1")
            });
        }

        WeeklyChart = new BarChart
        {
            Entries = entries,
            LabelTextSize = 30
        };

        // ✅ IMPORTANT (refresh UI)
        OnPropertyChanged(nameof(WeeklyChart));
    }
}