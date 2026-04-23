using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StayFitz.Models
{
    public class FitnessGoalModel : INotifyPropertyChanged
    {
        //public string Title { get; set; }
        //public string Icon { get; set; }

        //public double Current { get; set; }
        //public double Target { get; set; }

        //public string Goal { get; set; }

        //public double Progress => Target == 0 ? 0 : Current / Target;

        //public string Display => $"{Current} / {Target}";

        //public event PropertyChangedEventHandler PropertyChanged;

        //void OnPropertyChanged([CallerMemberName] string name = null)
        //    => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        private double current;

        public string Title { get; set; }
        public string Icon { get; set; }

        public double Target { get; set; }

        // ✅ When Current changes → auto update UI
        public double Current
        {
            get => current;
            set
            {
                current = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Progress));
                OnPropertyChanged(nameof(Display));
            }
        }

        // ✅ Calculated
        public double Progress => Target == 0 ? 0 : Current / Target;

        public string Display => $"{Current} / {Target}";

        public event PropertyChangedEventHandler PropertyChanged;

        // ✅ Keep it private (correct MVVM)
        void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}