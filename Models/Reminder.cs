using CommunityToolkit.Mvvm.ComponentModel;

namespace StayFitz.Models
{
    public partial class Reminder : ObservableObject
    {
        public int Id { get; set; }

        [ObservableProperty]
        private string message;

        [ObservableProperty]
        private string type;

        [ObservableProperty]
        private DateTime reminderDateTime;

        [ObservableProperty]
        private string musicPath;
    }

}


    