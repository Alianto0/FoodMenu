using AutoFixture.Xunit2;
using FluentAssertions;
using FoodMenu.Api.Clients;
using FoodMenu.Api.Exceptions;
using FoodMenu.Api.Logic;
using FoodMenu.Api.Models;
using Moq;
using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
using System.ComponentModel.DataAnnotations;

namespace FoodMenu.Api.UnitTests.Logic
{
    public class MealsRetrieverTests
    {
        [Theory]
        [AutoMockData]
        public async void GetMealByName_HappyPath_DoesNotThrow(MealsRetriever mealsRetriever)
        {
            // Arrange
            var testMealName = "Test Meal";

            // Act
            var response = await mealsRetriever.GetMealByName(testMealName);

            // Assert
            Assert.NotNull(response);
        }

        [Theory]
        [AutoMockData]
        public async void GetMealByName_WhenNoMealsFound_ThrowsMealNotFound(
            [Frozen] Mock<ITheMealDbClient> mealDbClient,
            MealsRetriever mealsRetriever)
        {
            // Arrange
            var testMealName = "Test Meal";

            mealDbClient.Setup(client => client.SearchMealByName(testMealName)).ReturnsAsync(new MealsDbResponse { Meals = [] });

            // Act
            Func<Task> response = async () => await mealsRetriever.GetMealByName(testMealName);

            // Assert
            await response.Should().ThrowAsync<MealNotFoundException>();
        }

        [Theory]
        [AutoMockData]
        public async void GetMealByName_WhenMultipleMealsFound_ReturnsFirst(
            MealsDbResponse mealsDbResponse,
            [Frozen] Mock<ITheMealDbClient> mealDbClient,
            MealsRetriever mealsRetriever)
        {
            // Arrange
            var testMealName = "Test Meal";
            mealDbClient.Setup(client => client.SearchMealByName(testMealName)).ReturnsAsync(mealsDbResponse);
            var expectedReturnedMeal = mealsDbResponse.Meals.First();

            // Act
            var response = await mealsRetriever.GetMealByName(testMealName);

            // Assert
            response.Area.Should().Be(expectedReturnedMeal.strArea);
            response.Category.Should().Be(expectedReturnedMeal.strCategory);
            response.Name.Should().Be(expectedReturnedMeal.strMeal);
        }

        [Theory]
        [AutoMockData]
        public async void GetMealByName_ReturnsExpectedNumberOfSuggestionsByCategory(
            [MinLength(10)]MealsDbMeal[] mealsDbmeals,
            [Frozen] Mock<ITheMealDbClient> mealDbClient,
            MealsRetriever mealsRetriever)
        {
            // Arrange
            const int expectedNumberOfSuggestions = 5;
            var testMealName = "Test Meal";
            mealDbClient.Setup(client => client.FilterMealByCategory(It.IsAny<string>())).ReturnsAsync(new MealsDbResponse { Meals = [.. mealsDbmeals]});   

            // Act
            var response = await mealsRetriever.GetMealByName(testMealName);

            // Assert
            response.SuggestionsByCategory.Should().HaveCount(expectedNumberOfSuggestions);
        }

        [Theory]
        [AutoMockData]
        public async void GetMealByName_ReturnsExpectedNumberOfSuggestionsByArea(
            [MinLength(10)] MealsDbMeal[] mealsDbmeals,
            [Frozen] Mock<ITheMealDbClient> mealDbClient,
            MealsRetriever mealsRetriever)
        {
            // Arrange
            const int expectedNumberOfSuggestions = 3;
            var testMealName = "Test Meal";
            mealDbClient.Setup(client => client.FilterMealByArea(It.IsAny<string>())).ReturnsAsync(new MealsDbResponse { Meals = [.. mealsDbmeals] });

            // Act
            var response = await mealsRetriever.GetMealByName(testMealName);

            // Assert
            response.SuggestionsByArea.Should().HaveCount(expectedNumberOfSuggestions);
        }
    }
}
