using System.Text;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using StayFitz.Helper;
using StayFitz.SSO;
using Android.Content;
using Plugin.LocalNotification.AndroidOption;
using Plugin.LocalNotification;

namespace StayFitz
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


#if SSO_ENABLED
            ClearCacheOfSSO();
#endif
            // Ensure the current activity is initialized
            //Microsoft.Maui.ApplicationModel.Platform.Init(this, savedInstanceState);

        }
        //protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        //{
        //    base.OnActivityResult(requestCode, resultCode, data);

        //    if (requestCode == 9001)
        //    {
        //        var account = StayFitz.Services.GoogleAuthService.GetAccountFromIntent(data);

        //        if (account != null)
        //        {
        //            string email = account.Email;
        //            string name = account.DisplayName;
        //            string idToken = account.IdToken;

        //            Android.Widget.Toast.MakeText(this, $"Welcome {name}", Android.Widget.ToastLength.Long).Show();
        //        }
        //    }
        //}

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public static async void ClearCacheOfSSO()
        {
            // Clear Global Token
            GlobalHelper.IdToken = string.Empty;
            using var stream = await FileSystem.OpenAppPackageFileAsync("appsettings.json");
            using var reader = new StreamReader(stream);
            string json = await reader.ReadToEndAsync();
            var builder = new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(json)));
            var configuration = builder.Build();

            string authority = string.Concat(
                configuration.GetValue<string>("Instance"),
                configuration.GetValue<string>("TenantId")
            );

            var application = PublicClientApplicationBuilder.Create(configuration.GetValue<string>("ClientId"))
                                                            .WithAuthority(authority)
                                                            .WithRedirectUri(CustomBrowserWebUi.FindFreeLocalhostRedirectUri()) // Custom redirect URI for the browser
                                                            .Build();

            var accounts = await application.GetAccountsAsync();

            if (accounts.Any())
            {
                var firstAccount = accounts.FirstOrDefault();
                if (firstAccount != null)
                {
                    await application.RemoveAsync(firstAccount); // Remove the first account from the cache
                }
            }
        }
    }
}
