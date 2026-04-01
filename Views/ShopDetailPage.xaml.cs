using StayFitz.Models;
using StayFitz.ViewModel;

namespace StayFitz.Views;

public partial class ShopDetailPage : ContentPage
{
	public ShopDetailPage(ShopItem Item)
	{
		InitializeComponent();
		BindingContext = new ShoptDetailViewModel(Item);
	}
}