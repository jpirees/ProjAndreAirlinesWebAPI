using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjAndreAirlinesWebAPI.Model;

namespace ProjAndreAirlinesWebAPITicket.Services
{
    public class FlightServiceAPI
    {
        public async Task<Flight> GetFlightAsync(Flight flightIn)
        {
            using HttpClient httpCliente = new();

            try
            {
                var response = await httpCliente.GetAsync($"https://localhost:44344/api/Flight/ico={flightIn.Origin.IataCode}&icd={flightIn.Destination.IataCode}&bt={flightIn.BoardingTime}");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                var flight = JsonConvert.DeserializeObject<Flight>(json);

                return flight ?? null;
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("Serviço indisponível.");
            }
        }
    }
}
