using System.Collections.Generic;
using MongoDB.Driver;
using ProjAndreAirlinesWebAPI.Model;
using ProjAndreAirlinesWebAPI.Utils;

namespace ProjAndreAirlinesWebAPIPassenger.Services
{
    public class PassengerService
    {
        private readonly IMongoCollection<Passenger> _passengers;

        public PassengerService(IProjAndreAirlinesDatabaseSettings settings)
        {
            var passenger = new MongoClient(settings.ConnectionString);
            var database = passenger.GetDatabase(settings.DatabaseName);
            _passengers = database.GetCollection<Passenger>(settings.CollectionName);
        }

        public List<Passenger> Get() =>
            _passengers.Find(passenger => true).ToList();

        public Passenger Get(string id) =>
            _passengers.Find<Passenger>(passenger => passenger.Id == id).FirstOrDefault();

        public Passenger GetByDocument(string cpf) =>
            _passengers.Find<Passenger>(passenger => passenger.Cpf == cpf).FirstOrDefault();

        public Passenger GetByPassaport(string passaportNumber) =>
            _passengers.Find<Passenger>(passenger => passenger.PassaportNumber == passaportNumber).FirstOrDefault();

        public Passenger Create(Passenger passengerIn)
        {
            _passengers.InsertOne(passengerIn);
            return passengerIn;
        }

        public void Update(string id, Passenger passengerIn) =>
            _passengers.ReplaceOne(passenger => passenger.Id == id, passengerIn);

        public void Remove(Passenger passengerIn) =>
            _passengers.DeleteOne(passenger => passenger.Id == passengerIn.Id);

        public void Remove(string id) =>
            _passengers.DeleteOne(passenger => passenger.Id == id);
    }
}
