using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using StayFitz.Models;

public class DietPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    public ObservableCollection<FoodModel> Foods { get; set; }
    public ObservableCollection<FoodModel> FilteredFoods { get; set; }
    public ObservableCollection<MealModel> TodayMeals { get; set; }

    public List<string> MealTypes { get; } = new() { "Breakfast", "Lunch", "Dinner" };

    private string _selectedMealType;
    public string SelectedMealType
    {
        get => _selectedMealType;
        set
        {
            _selectedMealType = value;
            OnPropertyChanged();
            FilterFoods(); 
        }
    }

    private FoodModel _selectedFood;
    public FoodModel SelectedFood
    {
        get => _selectedFood;
        set
        {
            _selectedFood = value;
            OnPropertyChanged();
        }
    }

    private int _dailyCalories;
    public int DailyCalories
    {
        get => _dailyCalories;
        set { _dailyCalories = value; OnPropertyChanged(); }
    }

    public ICommand AddMealCommand { get; }
    public ICommand DeleteMealCommand { get; }

    public DietPageViewModel()
    {
        Foods = new ObservableCollection<FoodModel>();
        FilteredFoods = new ObservableCollection<FoodModel>();
        TodayMeals = new ObservableCollection<MealModel>();

        AddMealCommand = new Command(AddMeal);
        DeleteMealCommand = new Command<MealModel>(DeleteMeal);

        LoadFoods();
    }

    void AddMeal()
    {
        if (SelectedFood == null || string.IsNullOrEmpty(SelectedMealType))
            return;

        TodayMeals.Add(new MealModel
        {
            MealType = SelectedFood.Name, 
            Calories = SelectedFood.Calories,
            Image = SelectedFood.Image,
            Food = SelectedFood,
            Time = DateTime.Now.ToString("hh:mm tt")
        });

        DailyCalories = TodayMeals.Sum(x => x.Calories); 

        SelectedFood = null;
    }

    void DeleteMeal(MealModel meal)
    {
        if (meal == null)
            return;

        TodayMeals.Remove(meal);
        DailyCalories = TodayMeals.Sum(x => x.Calories);
    }

    async void LoadFoods()
    {
        var service = new DietDataServices();
        var data = await service.GetDataAsync();

        if (data?.Foods != null)
        {
            Foods.Clear();
            foreach (var food in data.Foods)
                Foods.Add(food);
        }

       FilterFoods(); 
    }

    void FilterFoods()
    {
        FilteredFoods.Clear();

        if (string.IsNullOrEmpty(SelectedMealType))
            return;

        var filtered = Foods.Where(f =>
            !string.IsNullOrEmpty(f.MealType) &&
            f.MealType.Trim().ToLower() == SelectedMealType.Trim().ToLower()
        );

        foreach (var item in filtered)
            FilteredFoods.Add(item);

        SelectedFood = FilteredFoods.FirstOrDefault();

    }
}