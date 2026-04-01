using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayFitz.Models
{
    public class DailyMealModel
    {
        public string MealName { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }
        public int Calories { get; set; }
        public string Image { get; set; }
    }
}
