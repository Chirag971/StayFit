using System.Collections.ObjectModel;
using StayFitz.Controls;
using StayFitz.Helper;
using StayFitz.Models;
using StayFitz.Services;
using StayFitz.Utilities;
using StayFitz.ViewModels;

namespace StayFitz.Views;

[QueryProperty(nameof(WorkoutData), "WorkoutData")]
[QueryProperty(nameof(SelectedExercises), "SelectedExercises")]
public partial class ExercisePage : ContentPage
{
    WorkoutConfig _config;
    public List<ExerciseItem> SelectedExercises
    {
        set
        {
            if (value == null)
                return;

            ExerciseList.Clear();

            int index = 1;

            foreach (var item in value)
            {
                ExerciseList.Add(new ExerciseItem
                {
                    Id = index++,
                    Name = item.Name,
                    Icon = "",
                    Progress = 0,
                    Status = "Next-up",
                    StatusColor = Colors.Gray
                });
            }
        }
    }

    public WorkoutConfig WorkoutData
    {
        set => _config = value;
    }
   
 
    bool isPaused = false;
    bool isStopped = false;
    bool isWorkoutRunning = false;

    public ObservableCollection<ExerciseItem> ExerciseList { get; set; }
        = new ObservableCollection<ExerciseItem>();

    CircularProgressDrawable progressDrawable = new CircularProgressDrawable();

    public ExercisePage(ExercisePageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

        // Assign drawable to GraphicsView
        circularRing.Drawable = progressDrawable;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        

        if (_config == null)
        {
            await Shell.Current.DisplayAlert(
                MessageConstants.Error,
                MessageConstants.Workout,
                MessageConstants.Ok);
            return;
        }
        modeSwitch.IsToggled = _config.Mode == WorkoutMode.SetWise;
        lblMode.Text = modeSwitch.IsToggled
         ? "Set Wise Exercises"
         : "Exercise Wise Sets";
        //string mode = await Shell.Current.DisplayActionSheet(
        //    "Select Workout Mode",
        //    "Cancel",
        //    null,
        //    "Exercise Wise",
        //    "Set Wise");

        //if (mode == "Exercise Wise")
        //{
        //    modeSwitch.IsToggled = false;
        //    _config.Mode = WorkoutMode.ExerciseWise;
        //}
        //else if (mode == "Set Wise")
        //{
        //    modeSwitch.IsToggled = true;
        //    _config.Mode = WorkoutMode.SetWise;
        //}

        modeSwitch.IsEnabled = true;
        btnStart.IsEnabled = true;

        //LoadExercises();
    }
    async void OnStartClicked(object sender, EventArgs e)
    {
        if (isWorkoutRunning)
            return;

        isWorkoutRunning = true;

        isStopped = false;
        isPaused = false;

        btnPause.Text = "Pause";

        modeSwitch.IsEnabled = false;
        btnStart.IsEnabled = false;

        await StartWorkout();

        isWorkoutRunning = false;
        btnStart.IsEnabled = true;
    }
    //void LoadExercises()
    //{
    //    ExerciseList.Clear();

    //    var exercises = new (string Name, string Icon)[]
    //    {
    //        ("Push-Ups","push.png"),
    //        ("Squats","squats.png"),
    //        ("Plank","plank.png"),
    //        ("Lunges","lunges.png"),
    //        ("Burpees","burpees.png"),
    //        ("Jumping Jacks","jumpingjacks.png"),
    //        ("Mountain Climbers","mountain.png"),
    //        ("Sit-Ups","situp.png"),
    //        ("High Knees","highknees.png"),
    //        ("Leg Raises","legraises.png")
    //    };

    //    for (int i = 0; i < _config.Exercises; i++)
    //    {
    //        var exercise = exercises[i % exercises.Length];

    //        ExerciseList.Add(new ExerciseItem
    //        {
    //            Id = i + 1,
    //            Name = exercise.Name,
    //            Icon = exercise.Icon,
    //            Progress = 0,
    //            Status = "Next-up",
    //            StatusColor = Colors.Gray
    //        });
    //    }
    //}

    async Task StartWorkout()
    {
        modeSwitch.IsEnabled = false;

        if (modeSwitch.IsToggled)
        {
            await StartSetWise();
        }
        else
            await StartExerciseWise();

        modeSwitch.IsEnabled = true;

        if (!isStopped)
        {
            await Shell.Current.DisplayAlert(
                MessageConstants.WorkoutCompleted,
                MessageConstants.WorkoutCompletedMessage,
                MessageConstants.Ok);
        }
        if (!isStopped)
        {
            var history = new WorkoutHistory
            {
                Date = DateTime.Now,
                Mode = _config.Mode.ToString(),
                Sets = _config.Sets,
                Duration = _config.Duration,
                Exercises = ExerciseList.Select(x => x.Name).ToList()
            };

            // HistoryService.SaveHistory(history);
            //HistoryHelper.AddHistory(history);
            await HistoryService.SaveHistory(history);
            //await Shell.Current.DisplayAlert("Success", "Workout Saved!", "OK");
        }
    }
    //async Task StartWorkout()
    //{
    //    modeSwitch.IsEnabled = false;

    //    if (modeSwitch.IsToggled)
    //    {
    //        await StartSetWise();
    //    }
    //    else
    //    {
    //        await StartExerciseWise();
    //    }

    //    modeSwitch.IsEnabled = true;

    //    if (!isStopped)
    //    {
    //        // ✅ SAVE HISTORY HERE
    //        var history = new WorkoutHistory
    //        {
    //            Date = DateTime.Now,
    //            Mode = _config.Mode.ToString(),
    //            Sets = _config.Sets,
    //            Duration = _config.Duration,
    //            Exercises = ExerciseList.Select(x => x.Name).ToList()
    //        };

    //        await HistoryService.SaveHistory(history);

    //        await Shell.Current.DisplayAlert(
    //            "Workout Completed",
    //            "Saved to history ",
    //            "OK");
    //    }
    //}
    async Task StartSetWise()
    {
        for (int set = 1; set <= _config.Sets; set++)
        {
            if (isStopped)
                return;

            lblSetCount.Text = $"Set {set} / {_config.Sets}";

            for (int exerciseIndex = 0; exerciseIndex < ExerciseList.Count; exerciseIndex++)
            {
                if (isStopped)
                    return;
                var exercise = ExerciseList[exerciseIndex];

                lblExerciseCount.Text = $"Exercise {exerciseIndex + 1} / {ExerciseList.Count}";

                // Reset progress
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    exercise.Progress = 0;
                    exercise.Status = "In Progress";
                    exercise.StatusColor = Colors.Orange;
                });

                // Exercise timer
                await CircularTimer(_config.Duration, exercise);

                // Mark completed
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    if (!isStopped)
                    {
                        exercise.Progress = 1;
                        exercise.Status = $"Completed (Set {set})";
                        exercise.StatusColor = Colors.Lime;
                    }
                });

                // Rest between exercises
                if (exerciseIndex < ExerciseList.Count - 1)
                    await RunRestTimer(_config.Gap);
            }

            // Rest between sets
            if (set < _config.Sets)
                await RunRestTimer(_config.Gap);
        }
    }
    async Task StartExerciseWise()
    {
        for (int exerciseIndex = 0; exerciseIndex < ExerciseList.Count; exerciseIndex++)
        {
            if (isStopped)
                return;
            var exercise = ExerciseList[exerciseIndex];

            lblExerciseCount.Text = $"Exercise {exerciseIndex + 1} / {ExerciseList.Count}";

            for (int set = 1; set <= _config.Sets; set++)
            {
                if (isStopped)
                    return;
                lblSetCount.Text = $"Set {set} / {_config.Sets}";

                // 🔹 Reset progress for new set
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    exercise.Progress = 0;
                    exercise.Status = $"Set {set} In Progress";
                    exercise.StatusColor = Colors.Orange;
                });

                // Run exercise timer
                await CircularTimer(_config.Duration, exercise);

                // Mark set completed
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    if (!isStopped)
                    {
                        exercise.Progress = 1;
                        exercise.Status = $"Completed ({set}/{_config.Sets})";
                        exercise.StatusColor = Colors.Lime;
                    }
                });

                if (set < _config.Sets)
                    await RunRestTimer(_config.Gap);
            }

            if (exerciseIndex < ExerciseList.Count - 1)
                await RunRestTimer(_config.Gap);
        }
    }

    async Task CircularTimer(int seconds, ExerciseItem exercise)
    {
        progressDrawable.Progress = 1;
        circularRing.Invalidate();

        DateTime startTime = DateTime.UtcNow;
        DateTime endTime = startTime.AddSeconds(seconds);

        while (DateTime.UtcNow < endTime)
        {
            if (isStopped)
                return;

            if (isPaused)
            {
                await Task.Delay(50);
                continue;
            }

            double elapsed = (DateTime.UtcNow - startTime).TotalSeconds;
            double remaining = seconds - elapsed;
            double progress = Math.Clamp(remaining / seconds, 0, 1);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                lblTimer.Text = ((int)Math.Ceiling(remaining)).ToString();

                progressDrawable.Progress = progress;
                circularRing.Invalidate();

                // 🔥 update list progress
                exercise.Progress = 1 - progress;
            });

            await Task.Delay(50);
        }

        MainThread.BeginInvokeOnMainThread(() =>
        {
            lblTimer.Text = "0";
            progressDrawable.Progress = 0;
            circularRing.Invalidate();
        });
    }


    async Task RunRestTimer(int seconds)
    {
        lblTimer.Text = "REST";
        progressDrawable.Progress = 0;
        circularRing.Invalidate();

        for (int elapsed = 0; elapsed <= seconds; elapsed++)
        {
            if (isStopped)
                return;

            while (isPaused)
                await Task.Delay(200);

            double progress = (double)elapsed / seconds;

            MainThread.BeginInvokeOnMainThread(() =>
            {
                lblRestTimer.Text = $"00:{seconds - elapsed:D2}";

            });

            await Task.Delay(1000);
        }
    }

    void OnPauseClicked(object sender, EventArgs e)
    {
        isPaused = !isPaused;

        btnPause.Text = isPaused ? "Resume" : "Pause";

        audioBar.Pause(isPaused);
    }
    async void OnResetClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert(
            "Reset Workout",
            "Do you want to reset the workout?",
            "Yes",
            "No");

        if (!confirm)
            return;

        isStopped = true;
        isPaused = false;
        isWorkoutRunning = false;

        btnPause.Text = "Pause";

        MainThread.BeginInvokeOnMainThread(() =>
        {
            lblTimer.Text = "0";
            lblRestTimer.Text = "00:00";
            lblExerciseCount.Text = "Exercise 0 / 0";
            lblSetCount.Text = "Set 0 / 0";

            progressDrawable.Progress = 1;
            circularRing.Invalidate();
        });

        foreach (var ex in ExerciseList)
        {
            ex.Progress = 0;
            ex.Status = "Next-up";
            ex.StatusColor = Colors.Gray;
        }

        modeSwitch.IsEnabled = true;
        btnStart.IsEnabled = true;
    }
    protected override bool OnBackButtonPressed()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            bool exit = await DisplayAlert(
                "Stop Workout",
                "Workout will stop. Exit?",
                "Yes",
                "No");
  
            //await Shell.Current.GoToAsync("//ProductPage");
            if (exit)
            {
                isStopped = true;
                await Shell.Current.GoToAsync(nameof(HomePage));
            }
        });

        return true;
    }

    //void OnModeToggled(object sender, ToggledEventArgs e)
    //{
    //    if (_config == null)
    //        return;

    //    _config.Mode = e.Value
    //        ? WorkoutMode.SetWise
    //        : WorkoutMode.ExerciseWise;
    //}
    void OnModeToggled(object sender, ToggledEventArgs e)
    {
        if (e.Value)
        {
            lblMode.Text = "Set Wise Exercises";
            _config.Mode = WorkoutMode.SetWise;
        }
        else
        {
            lblMode.Text = "Exercise Wise Sets";
            _config.Mode = WorkoutMode.ExerciseWise;
        }
    }
    
}
