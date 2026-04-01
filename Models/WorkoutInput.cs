using CommunityToolkit.Mvvm.ComponentModel;

namespace StayFitz.Models
{
    public partial class WorkoutInput : ObservableObject
    {
        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private double value;

        [ObservableProperty]
        private double min;

        [ObservableProperty]
        private double max;
    }
}