﻿using FoodMenu.Api.Controllers;
using Microsoft.Extensions.Logging;
using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;

namespace FoodMenu.Api.UnitTests.Controllers
{
    public class MealsControllerTests
    {
        [Theory]
        [AutoMockData]
        public void GetMealByName_HappyPath_DoesNotThrow(ILogger<MealsController> logger)
        {
            var mealsController = new MealsController(logger);
            var testMealName = "Test Meal";

            var response = mealsController.GetMealByName(testMealName);

            Assert.NotNull(response);
        }
    }
}