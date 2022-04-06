using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MongoDB.Driver;
using Newtonsoft.Json;
using ProjAndreAirlinesWebAPI.Model;
using ProjAndreAirlinesWebAPI.Utils;

namespace ProjAndreAirlinesWebAPIBasePrice.Services
{
    public class BasePriceService
    {
        private readonly IMongoCollection<BasePrice> _basePrices;

        public BasePriceService(IProjAndreAirlinesDatabaseSettings settings)
        {
            var basePrice = new MongoClient(settings.ConnectionString);
            var database = basePrice.GetDatabase(settings.DatabaseName);
            _basePrices = database.GetCollection<BasePrice>(settings.CollectionName);
        }

        public List<BasePrice> Get() =>
            _basePrices.Find(basePrice => true).ToList();

        public BasePrice Get(string id) =>
            _basePrices.Find<BasePrice>(basePrice => basePrice.Id == id).FirstOrDefault();

        public BasePrice GetByAirports(string iataCodeOrigin, string iataCodeDestination) =>
            _basePrices.Find<BasePrice>(
                basePrice => basePrice.Origin.IataCode == iataCodeOrigin &&
                basePrice.Destination.IataCode == iataCodeDestination).FirstOrDefault();

        public BasePrice Create(BasePrice basePriceIn)
        {
            _basePrices.InsertOne(basePriceIn);
            return basePriceIn;
        }

        public void Update(string id, BasePrice basePriceIn) =>
            _basePrices.ReplaceOne(basePrice => basePrice.Id == id, basePriceIn);

        public void Remove(BasePrice basePriceIn) =>
            _basePrices.DeleteOne(basePrice => basePrice.Id == basePriceIn.Id);

        public void Remove(string id) =>
            _basePrices.DeleteOne(basePrice => basePrice.Id == id);


        public async Task<Airport> GetAiportByIataCode(string iataCode)
        {
            using HttpClient httpClient = new();

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:44363/api/Airport/{iataCode}");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                var airport = JsonConvert.DeserializeObject<Airport>(json);

                return airport;
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("Serviço indisponível.");
            }

        }
    }
}
