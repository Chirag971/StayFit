using StayFitz.ViewModel;

namespace StayFitz.Views;

public partial class DietTrackerPage : ContentPage
{
    public DietTrackerPage()
    {
        InitializeComponent();
        BindingContext = new DietTrackerViewModel();
    }
}