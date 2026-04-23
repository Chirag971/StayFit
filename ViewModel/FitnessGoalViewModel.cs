using System.Collections.ObjectModel;
using StayFitz.Models;

namespace StayFitz.ViewModel
{
    public class FitnessGoalViewModel
    {
        public ObservableCollection<FitnessGoalModel> Goals { get; set; }

        private readonly Random random = new Random();

        public FitnessGoalViewModel()
        {
            Goals = new ObservableCollection<FitnessGoalModel>
            {
                new FitnessGoalModel{ Title="Steps", Target=10000, Icon="steps.png" },
                new FitnessGoalModel{ Title="Calories", Target=800, Icon="calories.png" },
                new FitnessGoalModel{ Title="Heart Rate", Target=90, Icon="heartrate.png" },
                new FitnessGoalModel{ Title="Sleep", Target=8, Icon="sleep.png" }
            };

            LoadData();

            // ✅ Timer for auto update
            var timer = Application.Current.Dispatcher.CreateTimer();
            timer.Interval = TimeSpan.FromSeconds(3);

            timer.Tick += (s, e) =>
            {
                LoadData();
            };

            timer.Start();
        }

        private void LoadData()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                foreach (var goal in Goals)
                {
                    
                    goal.Current = random.Next(1, (int)goal.Target);

                    //goal.OnPropertyChanged(nameof(goal.Current));
                    //goal.OnPropertyChanged(nameof(goal.Progress));
                    //goal.OnPropertyChanged(nameof(goal.Display));
                }
            });
        }
    }
}