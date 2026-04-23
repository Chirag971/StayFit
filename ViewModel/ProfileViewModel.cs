using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using StayFitz.Models;
using StayFitz.Views;

namespace StayFitz.ViewModel
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        //public string UserName { get; set; } = "User";
        //public string Email { get; set; } = "user250@example.com";

        private string userName;
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged();
            }
        }

        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }



        public string ProfileImage { get; set; } = "user.png";
        public bool IsPremium { get; set; } = true;

        public ICommand NavigateCommand { get; }

        //
        public ObservableCollection<ProfileStatModel> Stats { get; set; }
        public ObservableCollection<SettingItemModel> Settings { get; set; }

        public ICommand EditProfileCommand { get; }
        public ICommand ViewProgressCommand { get; }

        public ProfileViewModel()
        {
            if (UserSession.CurrentUser != null)
            {
                UserName = UserSession.CurrentUser.Pin;     
                Email = UserSession.CurrentUser.Password;  
            }

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
                //new SettingItemModel{ Title="Notifications", Icon="notification.png"},
                new SettingItemModel{ Title="Reminders", Icon="reminder.png"},
                new SettingItemModel{ Title="Fitness Goals", Icon="goals.png"},
                new SettingItemModel { Title = "Settings ", Icon = "setting.png" },
                //new SettingItemModel{ Title="Favorite Activities", Icon="favorite.png"}
            };

            //EditProfileCommand = new Command(async () =>
            //{
            //    await Application.Current.MainPage.DisplayAlert(MessageConstants.EditProfileTitle,MessageConstants.EditProfileMessage,MessageConstants.Ok);
            //});

            //ViewProgressCommand = new Command(async () =>
            //{
            //    await Application.Current.MainPage.DisplayAlert(MessageConstants.Progress,MessageConstants.ViewProgressMessage, MessageConstants.Ok);
            //});

            NavigateCommand = new Command<SettingItemModel>(async (item) =>
            {
                if (item == null) return;

                if (item.Title == "Reminders")
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new ReminderPage());
                }
                if (item.Title == "Fitness Goals")
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new FitnessGoalPage());
                }
            });

            
        }
    }
}
