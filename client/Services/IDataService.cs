using System.Threading.Tasks;
using client.Pages;

namespace client.Services
{
    public interface IDataService
    {
        public Task<FetchData.WeatherForecast[]?> GetWeatherForecast();
    }
}