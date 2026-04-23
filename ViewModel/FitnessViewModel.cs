public class FitnessViewModel
{
    public FitnessDataModel Data { get; set; }

    private IDispatcherTimer _timer;
    Random random = new Random();

    public FitnessViewModel()
    {
        Data = new FitnessDataModel
        {
            Steps = 0,
            Calories = 0,
            HeartRate = 75,
            SleepHours = 6
        };

        StartSimulation();
    }
    void StartSimulation()
    {
        _timer = Application.Current.Dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromSeconds(3);

        _timer.Tick += (s, e) =>
        {
            Data.Steps += random.Next(5, 20);

            // FIX: Cast to int
            Data.Calories = (int)(Data.Steps * 0.04);

            Data.HeartRate = random.Next(70, 100);
        };
        _timer.Start();
    }
}