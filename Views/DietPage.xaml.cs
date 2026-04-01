using StayFitz.ViewModels;

namespace StayFitz.Views;

public partial class DietPage : ContentPage
{
	public DietPage(DietPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}