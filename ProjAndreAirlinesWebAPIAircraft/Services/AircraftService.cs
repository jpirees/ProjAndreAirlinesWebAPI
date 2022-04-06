using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using ProjAndreAirlinesWebAPI.Model;
using ProjAndreAirlinesWebAPI.Utils;

namespace ProjAndreAirlinesWebAPIAircraft.Services
{
    public class AircraftService
    {
        private readonly IMongoCollection<Aircraft> _aircrafts;

        public AircraftService(IProjAndreAirlinesDatabaseSettings settings)
        {
            var aircraft = new MongoClient(settings.ConnectionString);
            var database = aircraft.GetDatabase(settings.DatabaseName);
            _aircrafts = database.GetCollection<Aircraft>(settings.CollectionName);
        }

        public List<Aircraft> Get() =>
            _aircrafts.Find(aircraft => true).ToList();

        public Aircraft Get(string id) =>
            _aircrafts.Find<Aircraft>(aircraft => aircraft.Id == id).FirstOrDefault();

        public Aircraft GetByRegistrationCode(string registrationCode) =>
            _aircrafts.Find<Aircraft>(aircraft => aircraft.RegistrationCode == registrationCode).FirstOrDefault();

        public Aircraft Create(Aircraft aircraftIn)
        {
            _aircrafts.InsertOne(aircraftIn);
            return aircraftIn;
        }

        public void Update(string id, Aircraft aircraftIn) =>
            _aircrafts.ReplaceOne(aircraft => aircraft.Id == id, aircraftIn);

        public void Remove(Aircraft aircraftIn) =>
            _aircrafts.DeleteOne(aircraft => aircraft.Id == aircraftIn.Id);

        public void Remove(string id) =>
            _aircrafts.DeleteOne(aircraft => aircraft.Id == id);
    }
}
