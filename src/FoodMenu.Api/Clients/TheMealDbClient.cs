using FoodMenu.Api.Models;
using RestSharp;

namespace FoodMenu.Api.Clients
{
    public interface ITheMealDbClient
    {
        Task<MealsDbResponse> FilterMealByArea(string area);
        Task<MealsDbResponse> FilterMealByCategory(string category);
        Task<MealsDbResponse> SearchMealByName(string name);
    }

    public class TheMealDbClient : ITheMealDbClient
    {
        private const string baseUrl = "www.themealdb.com/api/json/v1/1";
        private readonly RestClient _client;
        public TheMealDbClient()
        {
            _client = new RestClient();
        }
        public async Task<MealsDbResponse> SearchMealByName(string name)
        {
            var uriBuilder = new UriBuilder($"{baseUrl}/search.php")
            {
                Query = $"?s={name}"
            };


            var restRequest = new RestRequest(uriBuilder.Uri.ToString());
            var response = await _client.GetAsync<MealsDbResponse>(restRequest);

            return response;
        }

        public async Task<MealsDbResponse> FilterMealByCategory(string category)
        {
            var uriBuilder = new UriBuilder($"{baseUrl}/filter.php")
            {
                Query = $"?c={category}"
            };

            var restRequest = new RestRequest(uriBuilder.Uri.ToString());
            var response = await _client.GetAsync<MealsDbResponse>(restRequest);
            return response;
        }

        public async Task<MealsDbResponse> FilterMealByArea(string area)
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
