using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StayFitz.Models;
using StayFitz.Views;
using StayFitz.Helper;
using StayFitz.Services;
using StayFitz.Utilities;
using Plugin.LocalNotification;

namespace StayFitz.ViewModels
{
    public partial class HomePageViewModel : ObservableObject
    {
       // private readonly NotificationService _notificationService;
        public ObservableCollection<Exercise> FilteredExercises { get; set; } = new();

        [ObservableProperty]
        bool isSuggestionVisible;
        //[RelayCommand]
        //public async Task OpenReminderPage()
        //{
        //    await Shell.Current.GoToAsync(nameof(ReminderPage));
        //}

        public List<Exercise> AllExercisesList { get; set; } = new();
        public void FilterExercises(string text)
        {
            FilteredExercises.Clear();

            if (string.IsNullOrWhiteSpace(text))
            {
                IsSuggestionVisible = false;
                return;
            }

            var results = AllExercisesList
                .Where(x => x.Name.ToLower().Contains(text.ToLower()))
                .ToList();

            foreach (var item in results)
                FilteredExercises.Add(item); 

            IsSuggestionVisible = results.Count > 0;
        }

    
        [ObservableProperty]
        string newExerciseName;


        public ObservableCollection<ExerciseItem> AllExercises { get; set; } = new();
        public ObservableCollection<ExerciseItem> VisibleExercises { get; set; } = new();
        public ObservableCollection<ExerciseItem> SelectedExercises { get; set; } = new();


        public ObservableCollection<WorkoutInput> Inputs { get; set; }

        public HomePageViewModel()
        {
           // _notificationService = new NotificationService();
            LoadExercises();

            Inputs = new ObservableCollection<WorkoutInput>
            {
                new WorkoutInput { Title="Number of Sets", Value=5, Min=1, Max=20},
                new WorkoutInput { Title="Duration (sec)", Value=10, Min=5, Max=60},
                new WorkoutInput { Title="Gap Between Sets", Value=3, Min=1, Max=30 }
            };
        }
        [RelayCommand]
        public void SetReminder()
        {
            // Example: 7:00 AM reminder
           // _notificationService.ScheduleDailyNotification(new TimeSpan(7, 0, 0));
        }
        private async void LoadExercises()
        {
            var data = await ExerciseService.GetExercisesAsync();

            if (data != null)
            {
                AllExercisesList = data;
            }
        }

        [RelayCommand]
        async Task AddExercise()
        {
            if (string.IsNullOrWhiteSpace(NewExerciseName))
            {
                await Shell.Current.DisplayAlert(MessageConstants.Error, "Enter exercise name", MessageConstants.Ok);
                return;
            }

            if (!AllExercisesList.Any(x => x.Name == NewExerciseName))
            {
                await Shell.Current.DisplayAlert("Invalid", "Please Enter valid exercise", "OK");
                return;
            }

            // Prevent duplicate
            if (AllExercises.Any(x => x.Name == NewExerciseName))
            {
                await Shell.Current.DisplayAlert("Duplicate", "Exercise already added", "OK");
                return;
            }

            AllExercises.Add(new ExerciseItem
            {
                Id = AllExercises.Count + 1,
                Name = NewExerciseName,

                // GET ICON FROM JSON LIST
                Icon = AllExercisesList
                        .FirstOrDefault(x => x.Name == NewExerciseName)?.Icon
            });

            NewExerciseName = string.Empty;
        }

   
        [RelayCommand]
        void DeleteExercise(ExerciseItem item)
        {
            if (item != null)
                AllExercises.Remove(item);
        }

     
        [RelayCommand]
        async Task GoToHistory()
        {
            await Shell.Current.GoToAsync(nameof(HistoryPage));
        }

       
        [ObservableProperty]
        string selectedMusicName = "Select Music";

        [RelayCommand]
        async Task SelectMusic()
        {
            try
            {
                var result = await FilePicker.Default.PickAsync();

                if (result != null)
                {
                    using Stream stream = await result.OpenReadAsync();
                    MemoryStream memoryStream = new MemoryStream();
                    await stream.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    GlobalHelper.MemoryStream = memoryStream;
                    SelectedMusicName = "🎵 " + result.FileName;
                }
            }
            catch { }
        }

        // ✅ START WORKOUT
        [RelayCommand]
        async Task StartWorkout()
        {
            var selectedExercises = AllExercises
                .Where(x => x.IsSelected)
                .ToList();

            if (selectedExercises.Count == 0)
            {
                await Shell.Current.DisplayAlert(
                    "Select Exercise",
                    "Please select at least one exercise",
                    "OK");
                return;
            }

            string mode = await Shell.Current.DisplayActionSheet(
                "Select Workout Mode",
                "Cancel",
                null,
                "Exercise Wise",
                "Set Wise");

            if (mode == "Cancel" || string.IsNullOrEmpty(mode))
                return;

            var config = new WorkoutConfig
            {
                Sets = (int)Inputs[0].Value,
                Duration = (int)Inputs[1].Value,
                Gap = (int)Inputs[2].Value,
                Mode = mode == "Set Wise"
                    ? WorkoutMode.SetWise
                    : WorkoutMode.ExerciseWise
            };

            await Shell.Current.GoToAsync(nameof(ExercisePage),
                new Dictionary<string, object>
                {
                    { "WorkoutData", config },
                    { "SelectedExercises", selectedExercises }
                });
        }
    }
}