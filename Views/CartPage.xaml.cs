using StayFitz.ViewModel;
using StayFitz.ViewModel.StayFitz.ViewModel;

namespace StayFitz.Views;

public partial class CartPage : ContentPage
{
	public CartPage()
	{
		InitializeComponent();
        BindingContext = new CartViewModel();

    }
  
}