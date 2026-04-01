using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using StayFitz.Models;
using Microsoft.Maui.Controls;

namespace StayFitz.ViewModels
{
    public partial class DietPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public string Greeting => "Good Morning, StayFitz!";

    
        private int _dailyCalories;
        public int DailyCalories
        {
            get => _dailyCalories;
            set
            {
                _dailyCalories = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Progress));
            }
        }

        public int GoalCalories { get; set; } = 2000;

        public double Progress => (double)DailyCalories / GoalCalories;

       
        public ObservableCollection<DietTipModel> DietTips { get; set; }
        public ObservableCollection<MealModel> TodayMeals { get; set; }

      
        public ICommand AddMealCommand { get; }

        public DietPageViewModel()
        {
            DietTips = new ObservableCollection<DietTipModel>();
            TodayMeals = new ObservableCollection<MealModel>();

            LoadData();

            AddMealCommand = new Command(AddMeal);
        }

        void LoadData()
        {
            // Diet Tips
            DietTips.Add(new DietTipModel
            {
                Title = "Balanced Meal Plan",
                Image = "meal.png"
            });

            DietTips.Add(new DietTipModel
            {
                Title = "Benefits of Hydration",
                Image = "water.png"
            });
            DietTips.Add(new DietTipModel
            {
                Title = "Benefits of Hydration",
                Image = "water.png"
            });
            // Meals
            TodayMeals.Add(new MealModel
            {
                MealType = "Breakfast",
                Time = "8:00 AM",
                Calories = 350,
                Image = "breakfast.png"
            });

            CalculateCalories();
        }

        void AddMeal()
        {
            TodayMeals.Add(new MealModel
            {
                MealType = "Lunch",
                Time = "1:00 PM",
                Calories = 500,
                Image = "lunch.png"
            });

            CalculateCalories();
        }

        void CalculateCalories()
        {
            DailyCalories = TodayMeals.Sum(x => x.Calories);
        }
    }
}