using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjAndreAirlinesWebAPI.Model;

namespace ProjAndreAirlinesWebAPI.Services
{
    public class ViaCepService
    {
        public static async Task<Address> SearchAddressByCep(string cep)
        {
            cep = cep.Replace("-", "").Replace(".", "");

            using HttpClient httpClient = new();

            try
            {
                httpClient.BaseAddress = new Uri("https://viacep.com.br/");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.GetAsync($"ws/{cep}/json/");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                var address = JsonConvert.DeserializeObject<Address>(json);

                return address ?? null;
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("Serviço indisponível.");
            }
        }
    }
}
