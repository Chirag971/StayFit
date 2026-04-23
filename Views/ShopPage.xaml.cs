using System.ComponentModel;
using StayFitz.Services;
using StayFitz.ViewModel;

namespace StayFitz.Views;

public partial class ShopPage : ContentPage, INotifyPropertyChanged
{
    public int CartCount => CartService.CartItems.Sum(x => x.Quantity);
    public ShopPage(ShopViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        CartService.CartUpdated += () =>
        {
            OnPropertyChanged(nameof(CartCount));
        };

    }
    private async void OnCartClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CartPage());
    }
    public new event PropertyChangedEventHandler PropertyChanged;

    void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}