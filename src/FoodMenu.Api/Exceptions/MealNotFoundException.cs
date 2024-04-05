namespace FoodMenu.Api.Exceptions
{
    /// <summary>
    /// Exception if the specified meal was not found.
    /// </summary>
    /// <param name="message">Message of the exception.</param>
    public class MealNotFoundException(string? message) : Exception(message)
    {
    }
}
