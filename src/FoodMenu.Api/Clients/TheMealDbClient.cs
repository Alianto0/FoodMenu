using FoodMenu.Api.Models;
using RestSharp;

namespace FoodMenu.Api.Clients
{
    public interface ITheMealDbClient
    {
        Task<MealsDbResponse?> FilterMealByArea(string area);
        Task<MealsDbResponse?> FilterMealByCategory(string category);
        Task<MealsDbResponse?> SearchMealByName(string name);
    }

    /// <summary>
    /// Client used to communicate with The Meal Db.
    /// </summary>
    public class TheMealDbClient : ITheMealDbClient
    {
        private const string baseUrl = "www.themealdb.com/api/json/v1/1";
        private readonly RestClient _client;
        public TheMealDbClient()
        {
            _client = new RestClient();
        }

        /// <summary>
        /// Returns meal by name.
        /// </summary>
        /// <param name="name">Name to search for.</param>
        /// <returns>Meals by name.</returns>
        public async Task<MealsDbResponse?> SearchMealByName(string name)
        {
            var uriBuilder = new UriBuilder($"{baseUrl}/search.php")
            {
                Query = $"?s={name}"
            };

            var restRequest = new RestRequest(uriBuilder.Uri.ToString());
            var response = await _client.GetAsync<MealsDbResponse>(restRequest);

            return response;
        }

        /// <summary>
        /// Returns meals by Category.
        /// </summary>
        /// <param name="category">Category to search for.</param>
        /// <returns>Meals in the Category.</returns>
        public async Task<MealsDbResponse?> FilterMealByCategory(string category)
        {
            var uriBuilder = new UriBuilder($"{baseUrl}/filter.php")
            {
                Query = $"?c={category}"
            };

            var restRequest = new RestRequest(uriBuilder.Uri.ToString());
            var response = await _client.GetAsync<MealsDbResponse>(restRequest);
            return response;
        }

        /// <summary>
        /// Returns meals by Area.
        /// </summary>
        /// <param name="category">Area to search for.</param>
        /// <returns>Meals in the Area.</returns>
        public async Task<MealsDbResponse?> FilterMealByArea(string area)
        {
            var uriBuilder = new UriBuilder($"{baseUrl}/filter.php")
            {
                Query = $"?a={area}"
            };

            var restRequest = new RestRequest(uriBuilder.Uri.ToString());
            var response = await _client.GetAsync<MealsDbResponse>(restRequest);
            return response;
        }
    }
}
