using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StayFitz.Models;
using StayFitz.Utilities;

namespace StayFitz.ViewModels
{

    public partial class LoginPageViewModel : ObservableObject
    {
        [ObservableProperty]
        string pin = "Admin123"; //USer Pin 

        [ObservableProperty]
        string password = "Admin@12";// User Password 

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
            // 🔹 Start Loader
            IsLoading = true;


            if (Pin == "Admin123" && Password == "Admin@12")
            {
                UserSession.CurrentUser = new UserModel
                {
                    Pin = Pin,
                    Password = Password,
                    Role = "Admin"
                };

                await Shell.Current.DisplayAlert(
                    MessageConstants.Success,
                    MessageConstants.LoginSuccessTitle,
                    MessageConstants.Ok);

                await ClearData();

                await Shell.Current.GoToAsync("//ProductPage");
            }
            else
            {
                await Shell.Current.DisplayAlert(
                    MessageConstants.Warning,
                    MessageConstants.InvalidCredential,
                    MessageConstants.Ok);

                await ClearData();
            }
        }
        finally
        {
            // 🔹 Stop Loader
            IsLoading = false;
        }
        }
        public async Task ClearData()
        {
            // Clear values
            Pin = string.Empty;
            Password = string.Empty;
        }
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

                await Shell.Current.DisplayAlert(MessageConstants.Warning,
               MessageConstants.OnlyAdminAccess,
                MessageConstants.Ok);
            }
            finally
            {
                IsLoading = false;
            }
            
           
        }

        private async Task SSoLoginAPI()
        {
            //var resoponse = await RestHelper.SSOLogin(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), GlobalHelper.IdToken);
            //if (resoponse.Success)
            //{
            //    GlobalHelper.User = resoponse.UserLoginData;
            //    GlobalHelper.User.AccessToken = resoponse.UserLoginData.AccessToken.Replace("Bearer ", String.Empty);
            //    GlobalHelper.User.TokenTimeStamp = DateTime.Now;

            //    // Check if Role is ADMIN (3)
            //    if (GlobalHelper.User.UserData.UserRole() == UserRole.Admin)
            //    {
            //        // Then go to Settings.
            //        FileLogger.Info("Admin Login successful.");
            //        await Shell.Current.GoToAsync(nameof(Settings));
            //    }
            //    //Check if Role is PUSER (6) or OPERATION (8)
            //    else if (GlobalHelper.User.UserData.UserRole() == UserRole.PUser || GlobalHelper.User.UserData.UserRole() == UserRole.Operation)
            //    {
            //        Helper.SaveUser(GlobalHelper.User.UserData);
            //        var RevenueCenter = Helper.GetRevenueCenter();
            //        if (!string.IsNullOrEmpty(RevenueCenter))
            //        {
            //            var responseDeviceSession = await RestHelper.GetDeviceSessionAsync(LPCore.Utilities.Constants.DEVICE_ID_CK, LPCore.Utilities.Constants.CL);
            //            if (responseDeviceSession.Success)
            //            {
            //                Helper.SaveIsEnrolled(true);
            //                GlobalHelper.Device = responseDeviceSession;
            //                GlobalHelper.Device.AccessToken = responseDeviceSession.AccessToken.Replace("Bearer ", String.Empty);
            //                GlobalHelper.Device.TokenTimeStamp = DateTime.Now;
            //                GlobalHelper.FiniteMachine.TransitionTo(FMSState.Ready);

            //                FileLogger.Info("PUser Login successful.");

            //                //create Local wallet
            //                await LPPOSHelper.CreateLocalWallet();

            //                //Store PackageData in memory
            //                var ResultPackages = await LPPOSRestHelper.GetPackageData(GlobalHelper.Device);

            //                if (ResultPackages.Success)
            //                {
            //                    LPPOSGlobalHelper.PackageData = ResultPackages.GetLPPackageData;
            //                }

            //                Helper.NavigateTo(new MainView(), AppShell.Current);
            //            }
            //            else
            //            {
            //                Helper.SaveIsEnrolled(false);
            //                await Shell.Current.DisplayAlert(MessagesConstants.Warning, MessagesConstants.DeveiceNotEnrolled, MessagesConstants.Ok);

            //                FileLogger.Error(MessagesConstants.DeveiceNotEnrolled);
            //                Helper.NavigateTo(new Login(), AppShell.Current);
            //            }
            //        }
            //        else
            //        {
            //            FileLogger.Error(MessagesConstants.ConfigureRVC);
            //            await Shell.Current.DisplayAlert(MessagesConstants.Warning, MessagesConstants.ConfigureRVC, MessagesConstants.Ok);
            //            Helper.NavigateTo(new Login(), AppShell.Current);
            //        }
            //    }
            //    else
            //    {
            //        FileLogger.Error("Unauthorize access: " + resoponse.Message);
            //    }
            //}
            //else
            //{
            //    await Shell.Current.DisplayAlert(MessagesConstants.Warning, resoponse.Message, MessagesConstants.Ok);
            //    Helper.NavigateTo(new Login(), AppShell.Current);
            //}
        }
        //[RelayCommand]
        //private async Task SSOLogin()
        //{
        //    try
        //    {
        //        IsNormalLoginVisible = false;
        //        IsLoading = true;
        //        await SSOHelper.SSOLogin();


        //        if (GlobalHelper.IdToken == null)
        //        {
        //            //await Shell.Current.DisplayAlert(MessagesConstants.Warning, "An error occured while do SSO Login.", MessagesConstants.Ok);
        //            //IsLoading = false;
        //            //IsNormalLoginVisible = true;
        //            return;
        //        }

        //        await SSoLoginAPI();

        //    }
        //    catch (Exception ex)
        //    {
        //        //await Shell.Current.DisplayAlert(MessagesConstants.Warning, "An error occurred during SSO login.", MessagesConstants.Ok);
        //        IsLoading = false;
        //        IsNormalLoginVisible = true;
        //    }
        //}



    }

}

