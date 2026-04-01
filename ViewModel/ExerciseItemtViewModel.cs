using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using StayFitz.Models;

namespace StayFitz.ViewModel
{
    public partial class ExerciseItemtViewModel : ObservableObject
    {
        public ObservableCollection<ExerciseItem> Exercises { get; set; }

        public ExerciseItemtViewModel()
        {
            Exercises = new ObservableCollection<ExerciseItem>
        {
            new ExerciseItem { Name="Push-Ups", Progress=1.0, Status="Completed" },
            new ExerciseItem { Name="Squats", Progress=0.5, Status="In Progress" },
            new ExerciseItem { Name="Plank", Progress=0.0, Status="Next Up" }
        };
        }
    }
}
