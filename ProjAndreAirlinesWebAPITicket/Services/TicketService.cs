using System.Collections.Generic;
using MongoDB.Driver;
using ProjAndreAirlinesWebAPI.Model;
using ProjAndreAirlinesWebAPI.Utils;

namespace ProjAndreAirlinesWebAPITicket.Services
{
    public class TicketService
    {
        private readonly IMongoCollection<Ticket> _tickets;

        public TicketService(IProjAndreAirlinesDatabaseSettings settings)
        {
            var ticket = new MongoClient(settings.ConnectionString);
            var database = ticket.GetDatabase(settings.DatabaseName);
            _tickets = database.GetCollection<Ticket>(settings.CollectionName);
        }

        public List<Ticket> Get() =>
            _tickets.Find(ticket => true).ToList();

        public Ticket Get(string id) =>
            _tickets.Find<Ticket>(ticket => ticket.Id == id).FirstOrDefault();

        public Ticket Create(Ticket ticketIn)
        {


            _tickets.InsertOne(ticketIn);
            
            return ticketIn;
        }

        public void Update(string id, Ticket ticketIn) =>
            _tickets.ReplaceOne(ticket => ticket.Id == id, ticketIn);

        public void Remove(Ticket ticketIn) =>
            _tickets.DeleteOne(ticket => ticket.Id == ticketIn.Id);

        public void Remove(string id) =>
            _tickets.DeleteOne(ticket => ticket.Id == id);
    }
}
