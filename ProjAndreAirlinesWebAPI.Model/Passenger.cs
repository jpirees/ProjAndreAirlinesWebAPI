using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjAndreAirlinesWebAPI.Model
{
    public class Passenger : Person
    {
       public string PassaportNumber { get; set; }
    }
}
