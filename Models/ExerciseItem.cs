using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace StayFitz.Models
{
    public partial class ExerciseItem:ObservableObject
    {
        //public int Id { get; set; }
        //public string Name { get; set; }
        //public double Progress { get; set; }  
        //public string Status { get; set; }
        //public bool IsVisible { get; set; }
        //public Color StatusColor { get; set; }
        //public string Icon { get; set; }


        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private double progress;

        [ObservableProperty]
        private string status;

        [ObservableProperty]
        private Color statusColor;

        [ObservableProperty]
        private string icon;
        public bool IsSelected { get; set; }


    }
}
