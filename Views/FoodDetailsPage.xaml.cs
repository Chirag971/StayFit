using StayFitz.Models;

namespace StayFitz.Views;

public partial class FoodDetailsPage : ContentPage
{
    public FoodDetailsPage(FoodModel food)  
    {
        InitializeComponent();
        BindingContext = food;
    }
}