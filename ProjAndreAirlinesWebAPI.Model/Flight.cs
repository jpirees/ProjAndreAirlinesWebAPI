using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjAndreAirlinesWebAPI.Model
{
    public class Flight
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Aircraft Aircraft { get; set; }
        public Airport Origin { get; set; }
        public Airport Destination { get; set; }
        public DateTime BoardingTime { get; set; }
        public DateTime LandingTime { get; set; }
    }
}
