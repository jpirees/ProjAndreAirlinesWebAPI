using System;
using System.Collections.Generic;
using MongoDB.Driver;
using ProjAndreAirlinesWebAPI.Model;
using ProjAndreAirlinesWebAPI.Utils;

namespace ProjAndreAirlinesWebAPIFlight.Services
{
    public class FlightService
    {
        private readonly IMongoCollection<Flight> _flights;

        public FlightService(IProjAndreAirlinesDatabaseSettings settings)
        {
            var flight = new MongoClient(settings.ConnectionString);
            var database = flight.GetDatabase(settings.DatabaseName);
            _flights = database.GetCollection<Flight>(settings.CollectionName);
        }

        public List<Flight> Get() =>
            _flights.Find(flight => true).ToList();

        public Flight Get(string id) =>
            _flights.Find<Flight>(flight => flight.Id == id).FirstOrDefault();

        public Flight GetFlight(string iataCodeOrigin, string iataCodeDestination, DateTime boardingTime) =>
            _flights.Find<Flight>(
                flight => flight.Origin.IataCode == iataCodeOrigin &&
                          flight.Destination.IataCode == iataCodeDestination && 
                          flight.BoardingTime == boardingTime).FirstOrDefault();

        public Flight Create(Flight flightIn)
        {
            _flights.InsertOne(flightIn);
            return flightIn;
        }

        public void Update(string id, Flight flightIn) =>
            _flights.ReplaceOne(flight => flight.Id == id, flightIn);

        public void Remove(Flight flightIn) =>
            _flights.DeleteOne(flight => flight.Id == flightIn.Id);

        public void Remove(string id) =>
            _flights.DeleteOne(flight => flight.Id == id);
    }
}
