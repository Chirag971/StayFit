using System.Text.Json.Serialization;
namespace StayFitz.Models
{
    public class FoodModel
    {
            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("calories")]
            public int Calories { get; set; }

            [JsonPropertyName("image")]

            public string Image { get; set; }

            [JsonPropertyName("description")]
            public string Description { get; set; }

            [JsonPropertyName("benefits")]

            public List<string> Benefits { get; set; }

            [JsonPropertyName("mealType")]
            public string MealType { get; set; } // 🔥 NEW

    }
}
