namespace FoodMenu.Api.Models
{
    public class Meal
    {
        public required string Name { get; set; }

        public required string Category { get; set; }

        public required string Area { get; set; }

        public List<SuggestionMeal> SuggestionsByCategory { get; set; } = new List<SuggestionMeal>();

        public List<SuggestionMeal> SuggestionsByArea { get; set; } = new List<SuggestionMeal>();
    }
}
