using FoodMenu.Api.Clients;
using FoodMenu.Api.Exceptions;
using FoodMenu.Api.Models;

namespace FoodMenu.Api.Logic
{
    public interface IMealsRetriever
    {
        Task<Meal> GetMealByName(string mealName);
    }

    /// <summary>
    /// Retrieves requested meals.
    /// </summary>
    /// <param name="mealDbClient"></param>
    public class MealsRetriever(ITheMealDbClient mealDbClient) : IMealsRetriever
    {
        private readonly ITheMealDbClient mealDbClient = mealDbClient;
        private const int SuggestedMealsByCategory = 5;
        private const int SuggestedMealsByArea = 3;

        /// <summary>
        /// Gets meal by tinput name.
        /// </summary>
        /// <param name="mealName">Name of the meal</param>
        /// <returns>Meal with suggestions.</returns>
        /// <exception cref="MealNotFoundException">Thrown if meal was not found.</exception>
        public async Task<Meal> GetMealByName(string mealName)
        {
            var response = await mealDbClient.SearchMealByName(mealName);

            if (response?.Meals == null || response.Meals.Count == 0)
            {
                throw new MealNotFoundException($"Meal with the name of '{mealName}' was not found");
            }

            var   responseMeal = response.Meals.Select(meal => new Meal { Area = meal.StrArea, Category = meal.StrCategory, Name = meal.StrMeal }).First();

            responseMeal.SuggestionsByCategory = await GetMealSuggestionsByCategory(responseMeal.Category);
            responseMeal.SuggestionsByArea = await GetMealSuggestionsByArea(responseMeal.Area);

            return responseMeal;
        }

        private async Task<List<SuggestionMeal>> GetMealSuggestionsByCategory(string category)
        {
            // caching may be added to improve overal performance
            var response = await mealDbClient.FilterMealByCategory(category);

            if(response == null)
            {
                return [];
            }

            List<SuggestionMeal> suggestionMeals;

            suggestionMeals = response.Meals.Select(meal => new SuggestionMeal { Name = meal.StrMeal }).Take(SuggestedMealsByCategory).ToList();            

            return suggestionMeals;
        }

        private async Task<List<SuggestionMeal>> GetMealSuggestionsByArea(string area)
        {
            // caching may be added to improve overal performance
            var response = await mealDbClient.FilterMealByArea(area);

            if (response == null)
            {
                return [];
            }

            List<SuggestionMeal> suggestionMeals;

            suggestionMeals = response.Meals.Select(meal => new SuggestionMeal { Name = meal.StrMeal }).Take(SuggestedMealsByArea).ToList();

            return suggestionMeals;
        }
    }
}
