using FoodMenu.Api.Models;
using RestSharp;

namespace FoodMenu.Api.Clients
{
    public interface ITheMealDbClient
    {
        Task<MealsDbResponse> SearchMealByName(string name);
    }

    public class TheMealDbClient : ITheMealDbClient
    {
        private RestClient _client;
        public TheMealDbClient()
        {
            _client = new RestClient();
        }

        public async Task<MealsDbResponse> SearchMealByName(string name)
        {
            var uriBuilder = new UriBuilder("www.themealdb.com/api/json/v1/1/search.php");
            uriBuilder.Query = $"?s={name}";


            var restRequest = new RestRequest(uriBuilder.Uri.ToString());
            var response = await _client.GetAsync<MealsDbResponse>(restRequest);
            return response;
        }
    }
}
