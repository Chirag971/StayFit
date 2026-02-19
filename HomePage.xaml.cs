namespace StayFit
{
    public partial class HomePage : ContentPage
    {
     
        public HomePage()
        {
            InitializeComponent();
        }
        private async void OnStartClicked(object sender, EventArgs e)
        {
            if (int.TryParse(txtExercises.Text, out int exerciseCount))
            {
                var data = new WorkoutConfig
                {
                    Exercises = exerciseCount,
                    Sets = int.Parse(txtSets.Text),
                    Duration = int.Parse(txtDuration.Text),
                    Gap = int.Parse(txtGap.Text)
                };

                await Navigation.PushAsync(new ExercisePage(data));
            }
        }

        private async void OnSelectMusic(object sender, EventArgs e)
        {
            await DisplayAlert("Music", "Music picker can be added later", "OK");
        }


    }

}
