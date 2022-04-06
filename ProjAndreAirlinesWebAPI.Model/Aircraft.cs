using System;

namespace ProjAndreAirlinesWebAPI.Model
{
    public class Aircraft
    {
        public string Id { get; set; }
        public string RegistrationCode { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
    }
}
