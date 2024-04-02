using Microsoft.AspNetCore.Mvc;

namespace FoodMenu.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class MealsController : ControllerBase
    {
        private readonly ILogger<MealsController> _logger;

        public MealsController(ILogger<MealsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{name}")]
        public string Get(string name)
        {
            return name;
        }
    }
}
