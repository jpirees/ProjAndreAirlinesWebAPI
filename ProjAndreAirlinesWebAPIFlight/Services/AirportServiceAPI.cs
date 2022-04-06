using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjAndreAirlinesWebAPI.Model;

namespace ProjAndreAirlinesWebAPIFlight.Services
{
    public class AirportServiceAPI
    {
        public async Task<Airport> GetAirportAsync(string iataCode)
        {
            using HttpClient httpCliente = new();

            try
            {
                var response = await httpCliente.GetAsync($"https://localhost:44363/api/Airport/{iataCode}");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                var airport = JsonConvert.DeserializeObject<Airport>(json);

                return airport ?? null;
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("Serviço indisponível.");
            }
        }
    }
}
