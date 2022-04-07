using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProjAndreAirlinesWebAPI.Model.DTO
{
    public class Airport
    {
        public static readonly string INSERT = "INSERT INTO Airport(Code, City, Country, Continent) VALUES(@Code, @City, @Country, @Continent)";
        public static readonly string GETALL = "SELECT Code, City, Country, Continent FROM Airport";
        public static readonly string GET = "SELECT Code, City, Country, Continent FROM Airport WHERE Code = @Code";


        [Key]
        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("City")]
        public string City { get; set; }

        [JsonProperty("Country")]
        public string Country { get; set; }

        [JsonProperty("Continent")]
        public string Continent { get; set; }

        public Airport() { }
    }
}
