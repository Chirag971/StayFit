using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StayFitz.Models
{
    public class RecipeModel
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("calories")]
        public int Calories { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("ingredients")]
        public List<string> Ingredients { get; set; }

        [JsonPropertyName("steps")]
        public List<string> Steps { get; set; }
    }
}
