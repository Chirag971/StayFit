using StayFitz.ViewModel;

namespace StayFitz.Views;

public partial class BalancedMealPage : ContentPage
{
    public BalancedMealPage()
    {
        InitializeComponent();
        BindingContext = new BalancedMealViewModel();
    }
}