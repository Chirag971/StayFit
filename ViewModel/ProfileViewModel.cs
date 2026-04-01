using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using StayFitz.Models;
using System.Windows.Input;
using StayFitz.Utilities;

namespace StayFitz.ViewModel
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        // Profile Info
        public string UserName { get; set; } = "abc";
        public string Email { get; set; } = "abc@example.com";
        public string ProfileImage { get; set; } = "profile.png";
        public bool IsPremium { get; set; } = true;

        // Collections
        public ObservableCollection<ProfileStatModel> Stats { get; set; }
        public ObservableCollection<SettingItemModel> Settings { get; set; }

        public ICommand EditProfileCommand { get; }
        public ICommand ViewProgressCommand { get; }

        public ProfileViewModel()
        {
            Stats = new ObservableCollection<ProfileStatModel>
            {
                new ProfileStatModel
                {
                    Title="Steps",
                    Value="6,540",
                    Icon="steps.png",
                    BackgroundColor="#D6F5E8"
                },
                new ProfileStatModel
                {
                    Title="Calories",
                    Value="785",
                    Icon="calories.png",
                    BackgroundColor="#E8F9E1"
                },
                new ProfileStatModel
                {
                    Title="Heart Rate",
                    Value="72 bpm",
                    Icon="heartrate.png",
                    BackgroundColor="#FFF1DA"
                },
                new ProfileStatModel
                {
                    Title="Sleep",
                    Value="8:10 hrs",
                    Icon="sleep.png",
                    BackgroundColor="#EDE7FF"
                }
            };

            Settings = new ObservableCollection<SettingItemModel>
            {
                new SettingItemModel{ Title="Notifications", Icon="notification.png"},
                new SettingItemModel{ Title="Reminders", Icon="reminder.png"},
                new SettingItemModel{ Title="Fitness Goals", Icon="goals.png"},
                new SettingItemModel{ Title="Favorite Activities", Icon="favorite.png"}
            };

            EditProfileCommand = new Command(async () =>
            {
                await Application.Current.MainPage.DisplayAlert(MessageConstants.EditProfileTitle,MessageConstants.EditProfileMessage,MessageConstants.Ok);
            });

            ViewProgressCommand = new Command(async () =>
            {
                await Application.Current.MainPage.DisplayAlert(MessageConstants.Progress,MessageConstants.ViewProgressMessage, MessageConstants.Ok);
            });
        }
    }
}
