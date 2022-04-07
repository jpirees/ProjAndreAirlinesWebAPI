using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjAndreAirlinesWebAPI.Model;

namespace ProjAndreAirlinesWebAPI.Services
{
    public class UserServiceAPI
    {
        public readonly HttpClient httpClient;

        public async Task<User> GetUserByDocument(string cpf)
        {
            try
            {
                httpClient.BaseAddress = new Uri("https://localhost:44376");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                
                HttpResponseMessage response = await httpClient.GetAsync($"/api/User/{cpf}/Profile");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                var user = JsonConvert.DeserializeObject<User>(json);

                return user ?? null;
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("Serviço indisponível.");
            }
        }

        public async Task<User> GetUserByUsername(string username)
        {
            try
            {
                httpClient.BaseAddress = new Uri("https://localhost:44376");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.GetAsync($"/api/User/{username}/Access");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                var user = JsonConvert.DeserializeObject<User>(json);

                return user ?? null;
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("Serviço indisponível.");
            }
        }
    }
}
