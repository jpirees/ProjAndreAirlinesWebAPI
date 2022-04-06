using System.Collections.Generic;
using MongoDB.Driver;
using ProjAndreAirlinesWebAPI.Model;
using ProjAndreAirlinesWebAPI.Utils;

namespace ProjAndreAirlinesWebAPITicketClass.Services
{
    public class TicketClassService
    {
        private readonly IMongoCollection<TicketClass> _ticketsClass;

        public TicketClassService(IProjAndreAirlinesDatabaseSettings settings)
        {
            var ticketClass = new MongoClient(settings.ConnectionString);
            var database = ticketClass.GetDatabase(settings.DatabaseName);
            _ticketsClass = database.GetCollection<TicketClass>(settings.CollectionName);
        }

        public List<TicketClass> Get() =>
            _ticketsClass.Find(ticketClass => true).ToList();

        public TicketClass Get(string id) =>
            _ticketsClass.Find<TicketClass>(ticketClass => ticketClass.Id == id).FirstOrDefault();

        public TicketClass GetByDescription(string description) =>
            _ticketsClass.Find<TicketClass>(ticketClass => ticketClass.Description == description).FirstOrDefault();

        public TicketClass Create(TicketClass ticketClassIn)
        {
            _ticketsClass.InsertOne(ticketClassIn);
            return ticketClassIn;
        }

        public void Update(string id, TicketClass ticketClassIn) =>
            _ticketsClass.ReplaceOne(ticketClass => ticketClass.Id == id, ticketClassIn);

        public void Remove(TicketClass ticketClassIn) =>
            _ticketsClass.DeleteOne(ticketClass => ticketClass.Id == ticketClassIn.Id);

        public void Remove(string id) =>
            _ticketsClass.DeleteOne(ticketClass => ticketClass.Id == id);
    }
}
