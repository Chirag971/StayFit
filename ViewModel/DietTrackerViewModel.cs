using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using StayFitz.Models;

namespace StayFitz.ViewModel
{
    public class DietTrackerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public int GoalCalories { get; set; } = 2000;

        private int _consumedCalories = 1210;
        public int ConsumedCalories
        {
            get => _consumedCalories;
            set
            {
                _consumedCalories = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Progress));
            }
        }

        public double Progress => (double)ConsumedCalories / GoalCalories;

        public ObservableCollection<MacroModel> Macros { get; set; }
        public ObservableCollection<DailyMealModel> Meals { get; set; }
        public ObservableCollection<WeeklyDayModel> WeekDays { get; set; }

        public DietTrackerViewModel()
        {
            LoadData();
        }

        void LoadData()
        {
            Macros = new ObservableCollection<MacroModel>
            {
                new MacroModel { Name="Carbs", Percentage=60, Color="#F6C343" },
                new MacroModel { Name="Proteins", Percentage=25, Color="#4DA3FF" },
                new MacroModel { Name="Fats", Percentage=15, Color="#D28BFF" }
            };

            Meals = new ObservableCollection<DailyMealModel>
            {
                new DailyMealModel
                {
                    MealName="Breakfast",
                    Time="8:00 AM",
                    Description="Scrambled eggs = vegetables",
                    Calories=360,
                    Image="breakfast.png"
                },
                new DailyMealModel
                {
                    MealName="Lunch",
                    Time="12:30 PM",
                    Description="Grilled chicken with quinoa",
                    Calories=450,
                    Image="lunch.png"
                },
                new DailyMealModel
                {
                    MealName="Snacks",
                    Time="8:00 PM",
                    Description="Greek yogurt with berries",
                    Calories=210,
                    Image="snack.png"
                }
            };

            WeekDays = new ObservableCollection<WeeklyDayModel>
            {
                new WeeklyDayModel{ Day="M" },
                new WeeklyDayModel{ Day="T" },
                new WeeklyDayModel{ Day="W" },
                new WeeklyDayModel{ Day="T" },
                new WeeklyDayModel{ Day="S", IsSelected=true },
                new WeeklyDayModel{ Day="S" }
            };
        }
    }
}
