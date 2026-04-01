using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayFitz.Models
{
    public class WorkoutHistory
    {
        public DateTime Date { get; set; }
        public string Mode { get; set; }
        public int Sets { get; set; }
        public int Duration { get; set; }
        public List<string> Exercises { get; set; }

        // ✅ NEW (IMPORTANT)
        public int TotalExercises => Exercises?.Count ?? 0;

        public int TotalTime => Sets * Duration;
    }
}
