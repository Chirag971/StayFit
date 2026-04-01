using StayFitz.ViewModel;

namespace StayFitz.Views;

public partial class ShopPage : ContentPage
{
	public ShopPage(ShopViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

	}
}