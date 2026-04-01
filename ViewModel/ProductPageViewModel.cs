using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using StayFitz.Models;
using StayFitz.Services;
using StayFitz.Utilities;
using StayFitz.ViewModel;
using StayFitz.Views;

namespace StayFitz.ViewModels
{
    public partial class ProductPageViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient = new HttpClient();        
        private readonly QuoteService _quoteService;

       

        [ObservableProperty]
        private string dailyMotivation;
        
       
        [RelayCommand]

        public async Task LoadQuoteAsync()
        {
            try
            {
                DailyMotivation = "Loading...";

                var request = new HttpRequestMessage(HttpMethod.Get, "https://zenquotes.io/api/random");

               
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0");

                var response = await _httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                var data = System.Text.Json.JsonSerializer.Deserialize<List<ZenQuote>>(json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                if (data?.Count > 0)
                {
                    DailyMotivation = $"\"{data[0].q}\"\n- {data[0].a}";
                }
                else
                {
                    DailyMotivation = "No quote found!";
                }
            }
            catch (Exception ex)
            {
                DailyMotivation = "Stay strong 💪";
                Console.WriteLine("ERROR: " + ex.Message);
            }
        }
        public ObservableCollection<HomeOption> Options { get; set; }
        [RelayCommand]
        private async Task GoToSettings()
        {
            await Shell.Current.GoToAsync(nameof(SettingsPage));
        }


        //[RelayCommand]
        //async Task Logout()
        //{
        //    bool result = await Shell.Current.DisplayAlert(
        //      MessageConstants.Warning,
        //      MessageConstants.LogoutMessage,
        //      MessageConstants.Yes,
        //      MessageConstants.No);

        //    if (result)
        //    {
        //        Preferences.Clear(); // clear saved data
        //        await Shell.Current.GoToAsync("//LoginPage");
        //    }
        //}

        [RelayCommand]
        private async Task GoToProfile()
        {
            await Shell.Current.GoToAsync(nameof(ProfilePage));
        }
        public ProductPageViewModel()
        {
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0");
            _quoteService = new QuoteService();

         
            Options = new ObservableCollection<HomeOption>
        {
            new HomeOption
            {
                Title="EXERCISE",
                Image="exercise.png",
                Color="#f08e51",
                Route=nameof(Views.HomePage)
            },
            new HomeOption
            {
                Title="DIET",
                Image="diet.png",
                Color="#7ccf81",
                Route=nameof(Views.DietPage)
            },
            new HomeOption
            {
                Title="HEALTH",
                Image="health.png",
                Color="#60acea",
                Route=nameof(Views.HealthPage)
            },
            new HomeOption
            {
                Title="SHOP",
                Image="box.png",
                Color="#a774d3",
                Route=nameof(Views.ShopPage)
            },
          
        };
        }

        [RelayCommand]
        public async Task OptionSelected(HomeOption option)
        {
            try
            {
                if (option.Route == nameof(HomePage))
                {
                    await Shell.Current.Navigation.PushAsync(new HomePage(IPlatformApplication.Current.Services.GetService<HomePageViewModel>()));
                }
                else if (option.Route == nameof(DietPage))
                {
                    await Shell.Current.Navigation.PushAsync(new DietPage(IPlatformApplication.Current.Services.GetService<DietPageViewModel>()));
                }
                else if (option.Route == nameof(HealthPage))
                {
                    await Shell.Current.Navigation.PushAsync(new HealthPage(IPlatformApplication.Current.Services.GetService<HealthPageViewModel>()));
                }
                else if (option.Route == nameof(ShopPage))
                {
                    await Shell.Current.Navigation.PushAsync(new ShopPage(IPlatformApplication.Current.Services.GetService<ShopViewModel>()));
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}