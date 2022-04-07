using System.Collections.Generic;
using System.Threading.Tasks;
using ProjAndreAirlinesWebAPI.Model.DTO;

namespace ProjAndreAirlinesWebAPIDapper.Repositories
{
    public interface IAirportRepository
    {
        public void Add(Airport airport);
        public List<Airport> GetAll();
        public Airport Get(string code);
    }
}
