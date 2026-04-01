
using StayFitz.Models;
using StayFitz.Utilities;
using StayFitz.ViewModels;


namespace StayFitz.Views;

public partial class ProductPage : ContentPage
{
    private ProductPageViewModel _viewModel;
    public ProductPage()
    {
        InitializeComponent();
        _viewModel = new ProductPageViewModel();
        BindingContext = _viewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (_viewModel != null)
        {
            await _viewModel.LoadQuoteAsync();
        }
      
        if (UserSession.CurrentUser == null ||
            UserSession.CurrentUser.Role != "Admin")
        {
            await Shell.Current.DisplayAlert(MessageConstants.Warning,
               MessageConstants.AdminOnlyAccessMessage,
                MessageConstants.Ok);

            await Shell.Current.GoToAsync("//LoginPage");
           
        }
    }

}