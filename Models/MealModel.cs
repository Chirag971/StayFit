using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayFitz.Models
{
    public class MealModel
    {
        public string MealType { get; set; }
        public string Time { get; set; }
        public int Calories { get; set; }
        public string Image { get; set; }
    }
}
