namespace StayFitz.Views;



[QueryProperty(nameof(ReminderId), "id")]
public partial class ReminderPage : ContentPage
{
    ReminderPageViewModel vm;

    public ReminderPage()
    {
        InitializeComponent();
        vm = new ReminderPageViewModel();
        BindingContext = vm;
    }

    private int reminderId;

    public int ReminderId
    {
        get => reminderId;
        set
        {
            reminderId = value;
            LoadReminder();
        }
    }

    private void LoadReminder()
    {
        vm.HandleNotification(reminderId);
    }
}