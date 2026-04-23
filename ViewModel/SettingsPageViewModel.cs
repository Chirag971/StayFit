using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace StayFitz.ViewModels
{
    //public partial class SettingsPageViewModel : INotifyPropertyChanged
    //{
    //    public ObservableCollection<string> Languages { get; set; }

    //    public ICommand UploadPhotoCommand { get; }

    //    private string selectedLanguage;
    //    public string SelectedLanguage
    //    {
    //        get => selectedLanguage;
    //        set
    //        {
    //            if (selectedLanguage != value)
    //            {
    //                selectedLanguage = value;
    //                OnPropertyChanged();
    //                SetCulture(selectedLanguage);
    //                Preferences.Set("AppLanguage", selectedLanguage);
    //            }
    //        }
    //    }

    //    public SettingsPageViewModel()
    //    {
    //        Languages = new ObservableCollection<string>
    //    {
    //        "English",
    //        "Hindi"
    //    };

    //        SelectedLanguage = Preferences.Get("AppLanguage", "English");
    //        SetCulture(SelectedLanguage);
    //    }

    //    private void SetCulture(string language)
    //    {
    //        if (language == "Hindi")
    //            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("hi");
    //        else
    //            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
    //    }

    //    public event PropertyChangedEventHandler PropertyChanged;
    //    protected void OnPropertyChanged([CallerMemberName] string name = null)
    //        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    //}
    public partial class SettingsPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> Languages { get; set; }

        public ICommand UploadPhotoCommand { get; }

        private ImageSource profileImage;
        public ImageSource ProfileImage
        {
            get => profileImage;
            set
            {
                profileImage = value;
                OnPropertyChanged();
            }
        }

        private string selectedLanguage;
        public string SelectedLanguage
        {
            get => selectedLanguage;
            set
            {
                if (selectedLanguage != value)
                {
                    selectedLanguage = value;
                    OnPropertyChanged();
                    SetCulture(selectedLanguage);
                    Preferences.Set("AppLanguage", selectedLanguage);
                }
            }
        }

        public SettingsPageViewModel()
        {
            Languages = new ObservableCollection<string>
            {
                "English",
                "Hindi"
            };

            UploadPhotoCommand = new Command(async () => await UploadPhoto());

            SelectedLanguage = Preferences.Get("AppLanguage", "English");
            SetCulture(SelectedLanguage);

            LoadProfileImage();
        }

        private void SetCulture(string language)
        {
            if (language == "Hindi")
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("hi");
            else
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");

            // Refresh whole app to update UI text
            Application.Current.MainPage = new AppShell();
        }

        private void LoadProfileImage()
        {
            var savedImage = Preferences.Get("ProfileImagePath", null);

            if (!string.IsNullOrEmpty(savedImage))
                ProfileImage = ImageSource.FromFile(savedImage);
            else
                ProfileImage = "profile.png";
        }

        private async Task UploadPhoto()
        {
            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Select Profile Image",
                FileTypes = FilePickerFileType.Images
            });

            if (result != null)
            {
                var newFile = Path.Combine(FileSystem.AppDataDirectory, result.FileName);

                using var stream = await result.OpenReadAsync();
                using var newStream = File.OpenWrite(newFile);
                await stream.CopyToAsync(newStream);

                ProfileImage = ImageSource.FromFile(newFile);
                Preferences.Set("ProfileImagePath", newFile);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}