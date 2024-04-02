using FoodMenu.Api.Logic;
using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;

namespace FoodMenu.Api.UnitTests.Logic
{
    public class MealsRetrieverTests
    {
        [Theory]
        [AutoMockData]
        public void GetMealByName_HappyPath_DoesNotThrow(MealsRetriever mealsRetriever)
        {
            var testMealName = "Test Meal";

            var response = mealsRetriever.GetMealByName(testMealName);

            Assert.NotNull(response);
        }
    }
}
