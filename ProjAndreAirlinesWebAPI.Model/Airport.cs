using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjAndreAirlinesWebAPI.Model
{
    public class Airport
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string IataCode { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
    }
}
