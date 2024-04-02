namespace FoodMenu.Api.Exceptions
{
    public class MealNotFoundException(string? message) : Exception(message)
    {
    }
}
