using StayFitz.ViewModels;

namespace StayFitz.Views;

public partial class SplashPage : ContentPage
{
    private readonly IServiceProvider _serviceProvider;
    public SplashPage(
        SplashPageViewModel viewModel,
        IServiceProvider serviceProvider)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _serviceProvider = serviceProvider;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await Task.Delay(3000);

        var productPage =
            _serviceProvider.GetRequiredService<ProductPage>();

        await Shell.Current.Navigation.PushAsync(productPage, true);

        //Application.Current.MainPage = productPage;
    }
}