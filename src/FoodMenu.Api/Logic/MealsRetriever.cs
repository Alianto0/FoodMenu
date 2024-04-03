using FoodMenu.Api.Clients;
using FoodMenu.Api.Exceptions;
using FoodMenu.Api.Models;

namespace FoodMenu.Api.Logic
{
    public interface IMealsRetriever
    {
        Task<Meal> GetMealByName(string mealName);
    }

    public class MealsRetriever : IMealsRetriever
    {
        private readonly ITheMealDbClient mealDbClient;
        private const int SuggestedMealsByCategory = 5;
        private const int SuggestedMealsByArea = 3;

        public MealsRetriever(ITheMealDbClient mealDbClient)
        {
            this.mealDbClient = mealDbClient;
        }

        public async Task<Meal> GetMealByName(string mealName)
        {
            var response = await mealDbClient.SearchMealByName(mealName);

            if (response.Meals == null || response.Meals.Count == 0)
            {
                throw new MealNotFoundException($"Meal with the name of '{mealName}' was not found");
            }

            var   responseMeal = response.Meals.Select(meal => new Meal { Area = meal.strArea, Category = meal.strCategory, Name = meal.strMeal }).First();

            responseMeal.SuggestionsByCategory = await GetMealSuggestionsByCategory(responseMeal.Category);
            responseMeal.SuggestionsByArea = await GetMealSuggestionsByArea(responseMeal.Area);

            return responseMeal;
        }

        private async Task<List<SuggestionMeal>> GetMealSuggestionsByCategory(string category)
        {
            var response = await mealDbClient.FilterMealByCategory(category);

            List<SuggestionMeal> suggestionMeals;

            suggestionMeals = response.Meals.Select(meal => new SuggestionMeal { Name = meal.strMeal }).Take(SuggestedMealsByCategory).ToList();            

            return suggestionMeals;
        }

        private async Task<List<SuggestionMeal>> GetMealSuggestionsByArea(string area)
        {
            var response = await mealDbClient.FilterMealByArea(area);

            List<SuggestionMeal> suggestionMeals;

            suggestionMeals = response.Meals.Select(meal => new SuggestionMeal { Name = meal.strMeal }).Take(SuggestedMealsByArea).ToList();

            return suggestionMeals;
        }
    }
}
