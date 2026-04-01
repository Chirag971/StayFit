using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using StayFitz.Models;
using StayFitz.Services;
using Plugin.Maui.Audio;
using StayFitz.Utilities;

public partial class ReminderPageViewModel : ObservableObject
{
    private readonly NotificationService _notificationService;
    private IAudioPlayer player;

    [ObservableProperty]
    private string musicName;
   
    public ObservableCollection<Reminder> Reminders { get; set; }

    public List<string> ReminderTypes { get; } = new()
    {
        "Workout",
        "Diet",
        "Yoga",
        "Water",
        "Sleep",
        "Morning walk"
    };
    [RelayCommand]
    public async Task DeleteReminder(Reminder reminder)
    {
        if (reminder == null)
            return;

        bool confirm = await Application.Current.MainPage.DisplayAlert(
            "Delete",
            "Are you sure you want to delete this reminder?",
            MessageConstants.Yes,
            MessageConstants.Ok);

        if (!confirm)
            return;

        // Cancel notification
        _notificationService.CancelNotification(reminder.Id);

        // Remove from list
        Reminders.Remove(reminder);

        // Save updated list
        ReminderStorage.SaveReminders(Reminders.ToList());
    }
    [ObservableProperty] private string selectedType;
    [ObservableProperty] private DateTime selectedDate = DateTime.Today;
    [ObservableProperty] private TimeSpan selectedTime = DateTime.Now.TimeOfDay;
    [ObservableProperty] private string message;
    [ObservableProperty] private string selectedMusicPath;
    [ObservableProperty] private Reminder selectedReminder;

    public void HandleNotification(int id)
    {
        var reminder = Reminders.FirstOrDefault(x => x.Id == id);

        if (reminder != null && !string.IsNullOrEmpty(reminder.MusicPath))
        {
            try
            {
                if (File.Exists(reminder.MusicPath)) // ✅ check
                {
                    player = AudioManager.Current.CreatePlayer(reminder.MusicPath);
                    player.Play();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("File not found");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Audio Error: {ex.Message}");
            }
        }
    }
    public ReminderPageViewModel()
    {

        _notificationService = new NotificationService();

        var list = ReminderStorage.GetReminders();

        Reminders = new ObservableCollection<Reminder>(list ?? new List<Reminder>());
    }
    private void LoadReminders()
    {
        var list = ReminderStorage.GetReminders();
        Reminders = new ObservableCollection<Reminder>(list);
    }

    // 🎵 PICK MUSIC (FIXED)
   [RelayCommand]
public async Task PickMusic()
{
    var result = await FilePicker.Default.PickAsync();

    if (result != null)
    {
        var newPath = Path.Combine(FileSystem.AppDataDirectory, result.FileName);

        using var stream = await result.OpenReadAsync();
        using var newStream = File.OpenWrite(newPath);
        await stream.CopyToAsync(newStream);

        SelectedMusicPath = newPath;
        MusicName = result.FileName;

        await Application.Current.MainPage.DisplayAlert("Selected", result.FileName, "OK");
    }
}

    // 💾 SAVE (ADD + EDIT)
    [RelayCommand]
    //public async Task SaveReminder()
    //{
    //    var dateTime = SelectedDate.Date + SelectedTime;

    //    if (dateTime <= DateTime.Now)
    //    {
    //        await Application.Current.MainPage.DisplayAlert("Error", "Select future time", "OK");
    //        return;
    //    }
    //    // ✏️ EDIT
    //    if (SelectedReminder != null)
    //    {
    //        _notificationService.CancelNotification(SelectedReminder.Id);

    //        SelectedReminder.Type = SelectedType;
    //        SelectedReminder.Message = Message;
    //        SelectedReminder.ReminderDateTime = dateTime;
    //        SelectedReminder.MusicPath = SelectedMusicPath;

    //        _notificationService.ScheduleNotification(SelectedReminder);

    //        // ✅ FORCE UI REFRESH
    //        var index = Reminders.IndexOf(SelectedReminder);
    //        Reminders.RemoveAt(index);
    //        Reminders.Insert(index, SelectedReminder);
    //    }
    //    else
    //    {
    //        // ➕ ADD
    //        var reminder = new Reminder
    //        {
    //            Id = new Random().Next(1000, 999999),
    //            Type = SelectedType,
    //            Message = Message,
    //            ReminderDateTime = dateTime,
    //            MusicPath = SelectedMusicPath
    //        };

    //        Reminders.Add(reminder);
    //        _notificationService.ScheduleNotification(reminder);
    //    }

    //    ReminderStorage.SaveReminders(Reminders.ToList());

    //    await Application.Current.MainPage.DisplayAlert("Saved", "Reminder saved!", "OK");

    //    ClearForm();
    //}


    public async Task SaveReminder()
    {
        var dateTime = SelectedDate.Date + SelectedTime;

        if (dateTime <= DateTime.Now)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Select future time", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(Message))
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Enter message", "OK");
            return;
        }

        if (SelectedReminder != null)
        {
            //  UPDATE EXISTING
            _notificationService.CancelNotification(SelectedReminder.Id);

            SelectedReminder.Message = Message;
            SelectedReminder.Type = SelectedType;
            SelectedReminder.ReminderDateTime = dateTime;
            SelectedReminder.MusicPath = SelectedMusicPath;

            _notificationService.ScheduleNotification(SelectedReminder);

            //  FORCE UI REFRESH
            //var index = Reminders.IndexOf(SelectedReminder);
            //Reminders.RemoveAt(index);
            //Reminders.Insert(index, SelectedReminder);

            await Application.Current.MainPage.DisplayAlert("Updated", "Reminder Updated!", "OK");
        }
        else
        {
            // ADD NEW
            var reminder = new Reminder
            {
                Id = new Random().Next(1000, 999999),
                Message = Message,
                Type = SelectedType ?? "Workout",
                ReminderDateTime = dateTime,
                MusicPath = SelectedMusicPath
            };

            Reminders.Add(reminder);

            _notificationService.ScheduleNotification(reminder);

            await Application.Current.MainPage.DisplayAlert("Saved", "Reminder Added!", "OK");
        }


        ReminderStorage.SaveReminders(Reminders.ToList());
        ClearForm();
    }


    [RelayCommand]
    public void EditReminder(Reminder reminder)
    {
        if (reminder == null) return;

        // Set selected reminder
        SelectedReminder = reminder;

        // Fill form fields
        Message = reminder.Message;
        SelectedType = reminder.Type;
        SelectedDate = reminder.ReminderDateTime.Date;
        SelectedTime = reminder.ReminderDateTime.TimeOfDay;
        SelectedMusicPath = reminder.MusicPath;

        // Optional: show music name
        MusicName = string.IsNullOrEmpty(reminder.MusicPath)
            ? ""
            : Path.GetFileName(reminder.MusicPath);
    }

    //  LOAD FOR EDIT
    //partial void OnSelectedReminderChanged(Reminder value)
    //{
    //    if (value == null) return;

    //    //SelectedReminder = value;

    //    Message = value.Message;
    //    SelectedType = value.Type;
    //    SelectedDate = value.ReminderDateTime.Date;
    //    SelectedTime = value.ReminderDateTime.TimeOfDay;
    //    SelectedMusicPath = value.MusicPath;
    //}

    private void ClearForm()
    {
        SelectedReminder = null;
        SelectedType = null;
        Message = "";
        SelectedMusicPath = "";
    }
}