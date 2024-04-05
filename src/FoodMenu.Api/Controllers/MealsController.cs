using FoodMenu.Api.Exceptions;
using FoodMenu.Api.Logic;
using FoodMenu.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodMenu.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class MealsController(IMealsRetriever mealsRetriever) : ControllerBase
    {
        private readonly IMealsRetriever mealsRetriever = mealsRetriever;

        /// <summary>
        /// Gets meal by name.
        /// </summary>
        /// <param name="name">Name of the meal to dsearch for.</param>
        /// <returns>Meal with suggested alternatives by category and area.</returns>       
        [HttpGet("{name}")]
        [ProducesResponseType(typeof(Meal), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMealByName(string name)
        {
            try
            {
                return Ok(await mealsRetriever.GetMealByName(name));
            }
            catch (MealNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ProblemDetails() { Title = "Not found", Status = StatusCodes.Status404NotFound, Detail = ex.Message });
            }
        }
    }
}
