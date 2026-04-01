using StayFitz.ViewModels;

namespace StayFitz.Views;

public partial class HealthPage : ContentPage
{
	public HealthPage(HealthPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
    //protected override async void OnAppearing()
    //{
    //    base.OnAppearing();
    //    await ((HealthPageViewModel)BindingContext).LoadArticlesAsync();
    //}
}