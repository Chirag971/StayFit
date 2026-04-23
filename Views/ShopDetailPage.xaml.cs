using StayFitz.Models;
using StayFitz.Models.StayFitz.Models;
using StayFitz.ViewModel;
using StayFitz.ViewModel.StayFitz.ViewModel;

namespace StayFitz.Views;

public partial class ShopDetailPage : ContentPage
{
	public ShopDetailPage(ShopItem Item)
	{
		InitializeComponent();
		BindingContext = new ShoptDetailViewModel(Item);
	}
    private async void OnCartClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CartPage());
    }
}