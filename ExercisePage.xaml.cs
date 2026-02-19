namespace StayFit;
    public partial class ExercisePage : ContentPage
{
    WorkoutConfig _config;
    int remainingSeconds;
    int currentExercise = 1;
    int currentSet = 1;
    public ExercisePage(WorkoutConfig config)
    {
        InitializeComponent();
        _config = config;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        StartWorkout();
    }

    async void StartTimer()
    {
        remainingSeconds = _config.Duration;

        while (remainingSeconds > 0)
        {
            lblTimer.Text = remainingSeconds.ToString();

            progressBar.Progress =
                (double)remainingSeconds / _config.Duration;

            await Task.Delay(1000);

            remainingSeconds--;
        }

        await DisplayAlert("Workout", "Set Completed!", "OK");

        await Navigation.PopAsync();
    }
    async void StartWorkout()
    {
        for (int exercise = 1; exercise <= _config.Exercises; exercise++)
        {
            lblExerciseCount.Text = $"Exercise {exercise} / {_config.Exercises}";

            for (int set = 1; set <= _config.Sets; set++)
            {
                lblSetCount.Text = $"Set {set} / {_config.Sets}";

                await RunWorkTimer(_config.Duration);

                if (set < _config.Sets)
                    await RunRestTimer(_config.Gap);
            }
        }

        await DisplayAlert("Workout", "Workout Completed 🎉", "OK");
        await Navigation.PopAsync();
    }
    async Task RunWorkTimer(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                lblTimer.Text = i.ToString();
                lblRestTimer.Text = "";
                progressBar.Progress = (double)i / seconds;
            });

            await Task.Delay(1000);
        }
    }

    async Task RunRestTimer(int seconds)
    {
        lblTimer.Text = "REST";

        for (int i = seconds; i > 0; i--)
        {
            lblRestTimer.Text = $"00:{i:D2}";
            progressBar.Progress = (double)i / seconds;

            await Task.Delay(1000);
        }
    }


}

