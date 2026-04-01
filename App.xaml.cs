using Plugin.LocalNotification;
using Plugin.LocalNotification.EventArgs;
using StayFitz.Views;
using Microsoft.Maui.ApplicationModel;

namespace StayFitz
{
    public partial class App : Application
    {
        public App()
        {
        InitializeComponent();

            //LocalNotificationCenter.Current.NotificationTapped += OnTapped;
           // LocalNotificationCenter.Current.NotificationTapped += (e) =>
           //{
           //    int id = int.Parse(e.Request.ReturningData);
           //    MainThread.BeginInvokeOnMainThread(async () =>
           //    {
           //        await Shell.Current.GoToAsync($"ReminderPage?id={id}");
           //    });
           //};
            MainPage = new AppShell();

    }
        private void OnTapped(NotificationEventArgs e)
        {
            int id = int.Parse(e.Request.ReturningData);

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync($"ReminderPage?id={id}");
            });
        }
        protected override async void OnStart()
        {
            base.OnStart();

            await Permissions.RequestAsync<Permissions.StorageRead>();
            await Permissions.RequestAsync<Permissions.PostNotifications>();
        }
        private void OnNotificationTapped(NotificationEventArgs e)
        {
            int id = int.Parse(e.Request.ReturningData);

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync($"ReminderPage?id={id}");
            });
        }

    }
}