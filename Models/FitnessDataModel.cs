using System.ComponentModel;
using System.Runtime.CompilerServices;

public class FitnessDataModel : INotifyPropertyChanged
{
    private int steps;
    private int calories;
    private int heartRate;
    private double sleepHours;

    public int Steps
    {
        get => steps;
        set { steps = value; OnPropertyChanged(); }
    }

    public int Calories
    {
        get => calories;
        set { calories = value; OnPropertyChanged(); }
    }

    public int HeartRate
    {
        get => heartRate;
        set { heartRate = value; OnPropertyChanged(); }
    }

    public double SleepHours
    {
        get => sleepHours;
        set { sleepHours = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged([CallerMemberName] string name = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}