using FoodMenu.Api.Clients;
using FoodMenu.Api.Exceptions;
using FoodMenu.Api.Models;

namespace FoodMenu.Api.Logic
{
    public interface IMealsRetriever
    {
        Task<List<Meal>> GetMealByName(string mealName);
    }

    public class MealsRetriever : IMealsRetriever
    {
        private readonly ITheMealDbClient mealDbClient;

        public MealsRetriever(ITheMealDbClient mealDbClient)
        {
            this.mealDbClient = mealDbClient;
        }

        public async Task<List<Meal>> GetMealByName(string mealName)
        {
            var response = await mealDbClient.SearchMealByName(mealName);

            List<Meal> responseMeals;

            if (response.Meals == null || response.Meals.Count == 0)
            {
                throw new MealNotFoundException($"Meal with the name of '{mealName}' was not found");
            }
            else
            {
                responseMeals = response.Meals.Select(meal => new Meal { Area = meal.strArea, Category = meal.strCategory, Name = meal.strMeal }).ToList();
              
            }

            return responseMeals;
        }
    }
}
