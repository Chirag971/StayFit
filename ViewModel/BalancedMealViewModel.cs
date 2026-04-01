using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using StayFitz.Models;
using StayFitz.Utilities;

namespace StayFitz.ViewModel
{
    public class BalancedMealViewModel 
    {
         
        public string HeaderImage => "meal.png";

        public ObservableCollection<MealComponentModel> Components { get; set; }
        public ObservableCollection<DailyMealModel> DailyMeals { get; set; }

        public ICommand LogSnackCommand { get; }

        public BalancedMealViewModel()
        {
            Components = new ObservableCollection<MealComponentModel>
            {
                new MealComponentModel
                {
                    Title = "Carbs",
                    Description = "Whole grains and vegetables",
                    Icon = "carbs.png"
                },
                new MealComponentModel
                {
                    Title = "Proteins",
                    Description = "Lean meats, fish, eggs",
                    Icon = "protein.png"
                },
                new MealComponentModel
                {
                    Title = "Vegetables",
                    Description = "Include colorful vegetables",
                    Icon = "vegetable.png"
                }
            };

            DailyMeals = new ObservableCollection<DailyMealModel>
            {
                new DailyMealModel
                {
                    MealName = "Breakfast",
                    Time = "8:00 AM",
                    Description = "Oatmeal with berries\nScrambled eggs with spinach",
                    Calories = 430
                },
                new DailyMealModel
                {
                    MealName = "Lunch",
                    Time = "12:30 PM",
                    Description = "Grilled chicken with quinoa",
                    Calories = 600
                }
            };

            LogSnackCommand = new Command(async () =>
            {
                await Application.Current.MainPage.DisplayAlert(MessageConstants.Info,MessageConstants.SnackLogged, MessageConstants.Ok);
            });
        }
    }
}