namespace StayFitz.Views;

public partial class ProgressPage : ContentPage
{
    ProgressViewModel vm = new ProgressViewModel();

    public ProgressPage()
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await vm.LoadProgress();
    }
}