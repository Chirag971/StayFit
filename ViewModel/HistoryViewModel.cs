using CommunityToolkit.Mvvm.ComponentModel;
using StayFitz.Models;
using StayFitz.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;



public partial class HistoryViewModel : ObservableObject
{
    public ObservableCollection<WorkoutHistory> HistoryList { get; set; }
        = new ObservableCollection<WorkoutHistory>();

    public ICommand DeleteCommand { get; }
    public ICommand ClearAllCommand { get; }

    public HistoryViewModel()
    {
        // ✅ DELETE SINGLE
        DeleteCommand = new Command<WorkoutHistory>(async (item) =>
        {
            if (item == null) return;

            bool confirm = await Application.Current.MainPage.DisplayAlert(
                "Delete",
                "Delete this workout?",
                "Yes",
                "No");

            if (!confirm) return;

            HistoryList.Remove(item);
            await HistoryService.DeleteHistory(item);
        });

        // ✅ CLEAR ALL
        ClearAllCommand = new Command(async () =>
        {
            bool confirm = await Application.Current.MainPage.DisplayAlert(
                "Clear All",
                "Delete ALL history?",
                "Yes",
                "No");

            if (!confirm) return;

            HistoryList.Clear();
            await HistoryService.ClearAll();
        });
    }

    // ✅ LOAD FROM JSON
    public async Task LoadHistory()
    {
        var list = await HistoryService.GetHistory();

        HistoryList.Clear();

        foreach (var item in list)
        {
            HistoryList.Add(item);
        }
    }
}

    //in memroy 

    // public IRelayCommand<WorkoutHistory> DeleteCommand { get; }

    // public HistoryViewModel()
    // {
    //     DeleteCommand = new RelayCommand<WorkoutHistory>(DeleteHistory);
    // }
    // public ObservableCollection<WorkoutHistory> HistoryList { get; set; }
    //= new ObservableCollection<WorkoutHistory>();

    // public void LoadHistory()
    // {
    //     var list = HistoryService.GetHistory();

    //     HistoryList.Clear();

    //     foreach (var item in list)
    //     {
    //         HistoryList.Add(item);
    //     }
    // }
    // private void DeleteHistory(WorkoutHistory item)
    // {
    //     if (item == null) return;


    //     HistoryList.Remove(item);

    //     HistoryService.DeleteHistory(item);
    // }


