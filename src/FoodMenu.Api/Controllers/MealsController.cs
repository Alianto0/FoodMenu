using FoodMenu.Api.Exceptions;
using FoodMenu.Api.Logic;
using FoodMenu.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodMenu.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class MealsController : ControllerBase
    {
        private readonly ILogger<MealsController> _logger;
        private readonly IMealsRetriever mealsRetriever;

        public MealsController(ILogger<MealsController> logger, IMealsRetriever mealsRetriever)
        {
            _logger = logger;
            this.mealsRetriever = mealsRetriever;
        }

        [HttpGet("{name}")]
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
