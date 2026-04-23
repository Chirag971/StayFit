using System.Text.Json.Serialization;

namespace StayFitz.Models
{
    public class DietDataModel
    {
        [JsonPropertyName("foods")]
        public List<FoodModel> Foods { get; set; }

        [JsonPropertyName("recipes")]
        public List<RecipeModel> Recipes { get; set; }
    }
}
