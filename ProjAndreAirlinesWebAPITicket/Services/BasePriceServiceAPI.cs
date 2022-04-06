using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjAndreAirlinesWebAPI.Model;

namespace ProjAndreAirlinesWebAPITicket.Services
{
    public class BasePriceServiceAPI
    {
        public async Task<BasePrice> GetBasePriceAsync(Flight flightIn)
        {
            using HttpClient httpCliente = new();

            try
            {
                var response = await httpCliente.GetAsync($"https://localhost:44313/api/BasePrice/{flightIn.Origin.IataCode}/{flightIn.Destination.IataCode}");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                var basePrice = JsonConvert.DeserializeObject<BasePrice>(json);

                return basePrice ?? null;
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("Serviço indisponível.");
            }
        }
    }
}
