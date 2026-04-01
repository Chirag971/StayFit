using StayFitz.Models;
using StayFitz.ViewModels;
namespace StayFitz.Views;

public partial class HomePage : ContentPage
{
	public HomePage(HomePageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
    private void OnExerciseSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is string selected)
        {
            var vm = BindingContext as HomePageViewModel;

            vm.NewExerciseName = selected;
            vm.IsSuggestionVisible = false;
        }
    }
    private void OnSuggestionSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Exercise selected)
        {
            var vm = BindingContext as HomePageViewModel;

            vm.NewExerciseName = selected.Name;

            vm.IsSuggestionVisible = false;

            ((CollectionView)sender).SelectedItem = null;
        }
    }
    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var vm = BindingContext as HomePageViewModel;

        if (vm != null)
        {
            vm.FilterExercises(e.NewTextValue);
        }
    }

}