using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using client.Pages;

namespace client.Services
{
    public class DataService: IDataService
    {
        private HttpClient _http;

        public DataService(HttpClient http)
        {
            _http = http;
        }

        public async Task<FetchData.WeatherForecast[]?> GetWeatherForecast()
        {
            return await _http.GetFromJsonAsync<FetchData.WeatherForecast[]>("sample-data/weather.json");
        }
    }
}