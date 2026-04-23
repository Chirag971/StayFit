using StayFitz.Models;
using StayFitz.Services;
using StayFitz.ViewModels;

namespace StayFitz.Views;

public partial class DietPage : ContentPage
{
    List<FoodModel> allFoods = new();

    public DietPage(DietPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadFoods();
    }

    async Task LoadFoods()
    {
        try
        {
            var service = new DietDataServices();
            var data = await service.GetDataAsync();

            if (data?.Foods != null)
            {
                allFoods = data.Foods;
            }
            else
            {
                await DisplayAlert("Error", "Foods not loaded", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    //async void OnMealSelected(object sender, SelectionChangedEventArgs e)
    //{
    //    try
    //    {
    //        var selectedMeal = e.CurrentSelection.FirstOrDefault() as MealModel;

    //        if (selectedMeal == null)
    //            return;

    //        var food = allFoods.FirstOrDefault(f => f.Name == selectedMeal.MealType);

    //        if (food == null)
    //        {
    //            await DisplayAlert("Error", "Food not found", "OK");
    //            return;
    //        }

    //        await Navigation.PushAsync(new FoodDetailsPage(food));

    //        ((CollectionView)sender).SelectedItem = null;
    //    }
    //    catch (Exception ex)
    //    {
    //        await DisplayAlert("Navigation Error", ex.Message, "OK");
    //    }
    //}

    //async void OnMealSelected(object sender, SelectionChangedEventArgs e)
    //{
    //    var selectedMeal = e.CurrentSelection.FirstOrDefault() as MealModel;

    //    if (selectedMeal == null)
    //        return;

    //    // ?? TEMP FIX (No error, always works)
    //    await Navigation.PushAsync(new FoodDetailsPage(new FoodModel
    //    {
    //        Name = selectedMeal.MealType,
    //        Calories = selectedMeal.Calories,
    //        Image = selectedMeal.Image
    //    }));

    //    ((CollectionView)sender).SelectedItem = null;
    //}
    async void OnMealSelected(object sender, SelectionChangedEventArgs e)
    {
        var selectedMeal = e.CurrentSelection.FirstOrDefault() as MealModel;

        if (selectedMeal == null)
            return;

        await Navigation.PushAsync(new FoodDetailsPage(selectedMeal.Food));

        ((CollectionView)sender).SelectedItem = null;
    }
}