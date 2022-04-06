using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjAndreAirlinesWebAPI.Model;

namespace ProjAndreAirlinesWebAPITicket.Services
{
    public class TicketClassServiceAPI
    {
        public async Task<TicketClass> GetTicketClassAsync(string description)
        {
            using HttpClient httpCliente = new();

            try
            {
                var response = await httpCliente.GetAsync($"https://localhost:44317/api/TicketClass/{description}");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                var ticketClass = JsonConvert.DeserializeObject<TicketClass>(json);

                return ticketClass ?? null;
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("Serviço indisponível.");
            }
        }
    }
}
