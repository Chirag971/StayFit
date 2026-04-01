using CommunityToolkit.Mvvm.Input;
using StayFitz.Helper;
using StayFitz.Utilities;
using StayFitz.Views;

namespace StayFitz
{
    public partial class AppShell : Shell
    {
        //public string ThemeText { get; set; }
        private string _themeText;
        public string ThemeText
        {
            get => _themeText;
            set
            {
                _themeText = value;
                OnPropertyChanged(nameof(ThemeText));
            }
        }
        private string _themeIcon;
        public string ThemeIcon
        {
            get => _themeIcon;
            set
            {
                _themeIcon = value;
                OnPropertyChanged(nameof(ThemeIcon));
            }
        }
        //DateTime lastBackPressed;
        public AppShell()
        {
            InitializeComponent();
            BindingContext = this;
            UpdateThemeText();
            Routing.RegisterRoute(nameof(ExercisePage), typeof(ExercisePage));
            AppRoutes.RegisterRoutes();
           
            //var content = new ShellContent();
            //content.Title = "StayFitz";
            //content.Content = new ProductPage(new ViewModels.ProductPageViewModel());
            //Items.Add(content);
        }

        bool isBackProcessing = false;

        protected override bool OnBackButtonPressed()
        {
            if (isBackProcessing)
                return true; // ignore multiple presses

            isBackProcessing = true;

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    if (Shell.Current.Navigation.NavigationStack.Count > 1)
                    {
                        await Shell.Current.Navigation.PopAsync();
                    }
                    else
                    {
                        await Shell.Current.GoToAsync("//ProductPage");
                    }
                }
                finally
                {
                    isBackProcessing = false;
                }
            });

            return true; // prevent default behavior
        }
        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert(
                 MessageConstants.Warning,
                "Are you sure you want to logout?",
                MessageConstants.Yes,
                MessageConstants.No);

            if (result)
            {
                Preferences.Clear();
                await Shell.Current.GoToAsync("//LoginPage");
            }
        }
        private void OnThemeChanged(object sender, EventArgs e)
        {
            if (Application.Current.UserAppTheme == AppTheme.Dark)
            {
                Application.Current.UserAppTheme = AppTheme.Light;
            }
            else
            {
                Application.Current.UserAppTheme = AppTheme.Dark;
            }

            UpdateThemeText();
        }
        private void UpdateThemeText()
        {
            var theme = Application.Current.UserAppTheme;

            if (theme == AppTheme.Unspecified)
                theme = Application.Current.RequestedTheme;

            if (theme == AppTheme.Dark)
            {
                ThemeText = "Dark Mode";
                ThemeIcon = "moon.png";   //add this image in Resources/Images
            }
            else
            {
                ThemeText = "Light Mode";
                ThemeIcon = "sun.png";    //add this image in Resources/Images
            }
        }
        //private void SetThemeUI()
        //{
        //    var theme = Application.Current.UserAppTheme;

        //    if (theme == AppTheme.Unspecified)
        //        theme = Application.Current.RequestedTheme;

        //    if (theme == AppTheme.Dark)
        //    {
        //        ThemeText = "Dark Theme 🌙";
        //        ThemeIcon = "moon.png"; // add this image
        //    }
        //    else
        //    {
        //        ThemeText = "Light Theme ☀️";
        //        ThemeIcon = "sun.png"; // add this image
    }
        }
   