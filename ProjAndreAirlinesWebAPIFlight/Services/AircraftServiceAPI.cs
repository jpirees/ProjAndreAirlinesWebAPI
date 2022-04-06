using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjAndreAirlinesWebAPI.Model;

namespace ProjAndreAirlinesWebAPIFlight.Services
{
    public class AircraftServiceAPI
    {
        public async Task<Aircraft> GetAircraftAsync(string registrationCode)
        {
            using HttpClient httpCliente = new();

            try
            {
                var response = await httpCliente.GetAsync($"https://localhost:44349/api/Aircraft/{registrationCode}");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                var aircraft = JsonConvert.DeserializeObject<Aircraft>(json);

                return aircraft ?? null;
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("Serviço indisponível.");
            }
        }
    }
}
