using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StayFitz.Models;
using UserSession = StayFitz.Models.UserSession;

namespace StayFitz.ViewModels
{
    public partial class LoginPageViewModel : ObservableObject
    {
        [ObservableProperty]
        string pin;

        [ObservableProperty]
        string password;

        [ObservableProperty]
        bool isLoading = false;

        [ObservableProperty]
        bool isNormalLoginVisible = true;

        [ObservableProperty]
        bool isSSOVisible = false;

        public LoginPageViewModel()
        {
#if SSO_ENABLED
            IsSSOVisible = true;
#endif
        }

     
        [RelayCommand]
        public async Task Back()
        {
            await Shell.Current.GoToAsync("//ProductPage");
        }

      
        [RelayCommand]
        private async Task Login()
        {
            try
            {
                IsLoading = true;

                if (string.IsNullOrWhiteSpace(Pin) || string.IsNullOrWhiteSpace(Password))
                {
                    await Shell.Current.DisplayAlert("Error", "Please enter Pin & Password", "OK");
                    return;
                }

                // ✅ Store user dynamically (NO STATIC CHECK)
                UserSession.CurrentUser = new UserModel
                {
                    Pin = Pin,
                    Password = Password,
                    Role = "User"
                };

                await Shell.Current.DisplayAlert(
                    "Success",
                    "Login Successful",
                    "OK");

                // ✅ Navigate to Profile Page
                await Shell.Current.GoToAsync("//ProfilePage");

                // ❗ Optional (clear AFTER navigation)
                // await ClearData();
            }
            finally
            {
                IsLoading = false;
            }
        }

     
        public Task ClearData()
        {
            Pin = string.Empty;
            Password = string.Empty;
            return Task.CompletedTask;
        }

        // 🔐 SSO Login
        [RelayCommand]
        async Task SSOLogin()
        {
            try
            {
                IsLoading = true;

                await Task.Delay(2000);

                UserSession.CurrentUser = new UserModel
                {
                    Pin = "SSOUser",
                    Password = "",
                    Role = "User"
                };

                await Shell.Current.DisplayAlert(
                    "Info",
                    "Logged in via SSO",
                    "OK");

                // ✅ Navigate to Profile
                await Shell.Current.GoToAsync("//ProfilePage");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}