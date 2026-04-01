using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayFitz.Models
{
    public class WorkoutConfig
    {
            public int Exercises { get; set; }
            public int Sets { get; set; }
            public int Duration { get; set; }
            public int Gap { get; set; }
        public WorkoutMode Mode { get; set; } = WorkoutMode.ExerciseWise;
    }
}
