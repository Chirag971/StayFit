namespace StayFitz.Views;

public partial class SetGoalPage : ContentPage
{
	public SetGoalPage()
	{
		InitializeComponent();
		BindingContext = new SetGoalViewModel();

	}
}