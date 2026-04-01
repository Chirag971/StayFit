using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StayFitz.Models;
using StayFitz.Services;

namespace StayFitz.ViewModels
{

    public partial class ExercisePageViewModel : ObservableObject
    {
        public ExercisePageViewModel()
        {
            
        }

        [RelayCommand]
        async Task Back()
        {
            await Shell.Current.Navigation.PopAsync();
        }

        public class ExerciseViewModel : ObservableObject
        {
            public ObservableCollection<IGrouping<string, Exercise>> GroupedExercises { get; set; }

            public ObservableCollection<string> SelectedExercises { get; set; } = new();

            public async Task LoadExercises()
            {
                var list = await ExerciseService.GetExercisesAsync();

                var grouped = list.GroupBy(x => x.Category);

                GroupedExercises = new ObservableCollection<IGrouping<string, Exercise>>(grouped);

                OnPropertyChanged(nameof(GroupedExercises));
            }

            public ICommand SelectExerciseCommand => new Command<Exercise>((exercise) =>
            {
                if (!SelectedExercises.Contains(exercise.Name))
                {
                    SelectedExercises.Add(exercise.Name);
                }
            });
        }

    }
 }