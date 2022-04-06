using System;

namespace ProjAndreAirlinesWebAPI.Utils
{
    public class ProjAndreAirlinesDatabaseSettings: IProjAndreAirlinesDatabaseSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
