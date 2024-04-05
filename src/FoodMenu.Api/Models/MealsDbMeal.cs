using System.Text.Json.Serialization;

namespace FoodMenu.Api.Models
{
    public class MealsDbMeal
    {
        [JsonPropertyName("idMeal")]
        public int IdMeal { get; set; }

        [JsonPropertyName("strMeal")]
        public required string StrMeal { get; set; }

        [JsonPropertyName("strCategory")]
        public string? StrCategory { get; set; }

        [JsonPropertyName("strArea")]
        public string? StrArea { get; set; }
    }
}

