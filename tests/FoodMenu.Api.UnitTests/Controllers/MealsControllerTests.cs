using FluentAssertions;
using FoodMenu.Api.Controllers;
using FoodMenu.Api.Exceptions;
using FoodMenu.Api.Logic;
using FoodMenu.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;

namespace FoodMenu.Api.UnitTests.Controllers
{
    public class MealsControllerTests
    {
        [Theory]
        [AutoMockData]
        public async void GetMealByName_HappyPath_DoesNotThrow(ILogger<MealsController> logger, IMealsRetriever mealsRetriever)
        {
            const string testMealName = "Test Meal";
            var mealsController = new MealsController(logger, mealsRetriever);

            // Act
            var response = await mealsController.GetMealByName(testMealName);
            var statusCodeResult = response as OkObjectResult;

            // Assert
            statusCodeResult.Should().NotBeNull();
            statusCodeResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            statusCodeResult.Value.Should().NotBeNull();
            statusCodeResult.Value.Should().BeOfType<Meal>();
        }

        [Theory]
        [AutoMockData]
        public async void GetMealByName_MealsRetrieverThrowingMealNotFoundException_Returns404(ILogger<MealsController> logger, Mock<IMealsRetriever> mealsRetriever)
        {
            // Arrange
            const string testMealName = "Test Meal";
            mealsRetriever.Setup(mealsRetriever => mealsRetriever.GetMealByName(It.IsAny<string>())).ThrowsAsync(new MealNotFoundException(testMealName));

            var mealsController = new MealsController(logger, mealsRetriever.Object);

            // Act
            var response = await mealsController.GetMealByName(testMealName);
            var statusCodeResult = response as ObjectResult;

            // Assert
            statusCodeResult.Should().NotBeNull();
            statusCodeResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            statusCodeResult.Value.Should().NotBeNull();
            statusCodeResult.Value.Should().BeOfType<ProblemDetails>();
        }
    }
}
