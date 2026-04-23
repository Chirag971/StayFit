using StayFitz.Models;
namespace StayFitz.Views;

public partial class FoodsPage : ContentPage
{
	public List<FoodModel> Foods { get; set; }
    public FoodsPage()
	{
		InitializeComponent();
        LoadData();
	}

    //async void OnViewClicked(object sender, EventArgs e)
    //{
    //    var button = sender as Button;

    //    if (button?.BindingContext is FoodModel food)
    //    {
    //        await Navigation.PushAsync(new FoodDetailsPage(food));
    //    }
    //}
    async void LoadData()
    {
        var service = new DietDataServices();
        var data = await service.GetDataAsync();

        if (data == null)
        {
            await DisplayAlert("Error", "JSON not loaded", "OK");
            return;
        }

        if (data.Foods == null)
        {
            await DisplayAlert("Error", "Foods null", "OK");
            return;
        }

        Foods = data.Foods;
        BindingContext = this;
    }
    async void OnItemTapped(object sender, EventArgs e)
    {
        var frame = sender as Frame;

        if (frame?.BindingContext is FoodModel food)
        {
            await Navigation.PushAsync(new FoodDetailsPage(food));
        }
    }
}



