namespace StayFitz.Models
{
    public class MealModel
    {
       
        public int Id { get; set; }
        public string MealType { get; set; }
        public string Time { get; set; }
        public int Calories { get; set; }
        public string Image { get; set; }
        public FoodModel Food { get; set; }
    }
}
