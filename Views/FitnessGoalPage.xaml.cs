using StayFitz.ViewModel;

namespace StayFitz.Views;

public partial class FitnessGoalPage : ContentPage
{
	public FitnessGoalPage()
	{
		InitializeComponent();
        BindingContext = new FitnessGoalViewModel();

    }
    public async void OnSetGoalClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SetGoalPage());
    }
}