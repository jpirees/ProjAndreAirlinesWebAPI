using System.Collections.Generic;
using MongoDB.Driver;
using ProjAndreAirlinesWebAPI.Model;
using ProjAndreAirlinesWebAPI.Utils;

namespace ProjAndreAirlinesWebAPIAirport.Services
{
    public class AirportService
    {
        private readonly IMongoCollection<Airport> _airports;

        public AirportService(IProjAndreAirlinesDatabaseSettings settings)
        {
            var airport = new MongoClient(settings.ConnectionString);
            var database = airport.GetDatabase(settings.DatabaseName);
            _airports = database.GetCollection<Airport>(settings.CollectionName);
        }

        public List<Airport> Get() =>
            _airports.Find(airport => true).ToList();

        public Airport Get(string id) =>
            _airports.Find<Airport>(airport => airport.Id == id).FirstOrDefault();
       
        public Airport GetByIataCode(string iataCode) =>
           _airports.Find<Airport>(airport => airport.IataCode == iataCode).FirstOrDefault();

        public Airport Create(Airport airportIn)
        {
            _airports.InsertOne(airportIn);
            return airportIn;
        }

        public void Update(string id, Airport airportIn) =>
            _airports.ReplaceOne(airport => airport.Id == id, airportIn);

        public void Remove(Airport airportIn) =>
            _airports.DeleteOne(airport => airport.Id == airportIn.Id);

        public void Remove(string id) =>
            _airports.DeleteOne(airport => airport.Id == id);

    }
}
