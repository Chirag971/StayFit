using System.Windows.Input;

public class SetGoalViewModel
{
    public string StepsGoal { get; set; }
    public string CaloriesGoal { get; set; }
    public string SleepGoal { get; set; }

    public ICommand SaveCommand { get; }

    public SetGoalViewModel()
    {
        SaveCommand = new Command(SaveGoals);
    }

    void SaveGoals()
    {
        Preferences.Set("StepsGoal", StepsGoal);
        Preferences.Set("CaloriesGoal", CaloriesGoal);
        Preferences.Set("SleepGoal", SleepGoal);
      

        Application.Current.MainPage.DisplayAlert("Success", "Goals Saved!", "OK");
    }
}