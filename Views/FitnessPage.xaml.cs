namespace StayFitz.Views;

public partial class FitnessPage : ContentPage
{
    public FitnessPage()
    {
        InitializeComponent();
        BindingContext = new FitnessViewModel();
    }
}