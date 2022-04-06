using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace ProjAndreAirlinesWebAPI.Model
{
    public class Address
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [JsonProperty("cep")]
        public string Cep { get; set; }

        [JsonProperty("logradouro")]
        public string Street { get; set; }

        public int Number { get; set; }

        [JsonProperty("bairro")]
        public string District { get; set; }

        [JsonProperty("localidade")]
        public string City { get; set; }

        [JsonProperty("uf")]
        public string State { get; set; }
    }
}
