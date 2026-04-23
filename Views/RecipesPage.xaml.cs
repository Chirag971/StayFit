using StayFitz.Models;

namespace StayFitz.Views;

public partial class RecipesPage : ContentPage
{
	public RecipesPage()
	{
		InitializeComponent();
	}

    private async void OnViewRecipeClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var recipe = button?.BindingContext as RecipeModel;

        if (recipe == null) return;

        string ingredients = string.Join("\n", recipe.Ingredients);
        string steps = string.Join("\n", recipe.Steps);

        await Application.Current.MainPage.DisplayAlert(
            recipe.Title,
            $"Ingredients:\n{ingredients}\n\nSteps:\n{steps}",
            "OK"
        );
    }
}