using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjAndreAirlinesWebAPI.Model
{
    public class Ticket
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Flight Flight { get; set; }
        public BasePrice BasePrice { get; set; }
        public TicketClass TicketClass { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    }
}
