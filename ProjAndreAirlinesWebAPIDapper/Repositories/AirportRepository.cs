using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using ProjAndreAirlinesWebAPI.Model.DTO;
using ProjAndreAirlinesWebAPIDapper.Config;

namespace ProjAndreAirlinesWebAPIDapper.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        private readonly string _connection;

        public AirportRepository(string connection)
        {
            _connection = connection;
        }

        public void Add(Airport airport)
        {
            using (var db = new SqlConnection(_connection))
            {
                db.Open();
                db.Execute(Airport.INSERT, airport);
            }
        }

        public List<Airport> GetAll()
        {
            using (var db = new SqlConnection(_connection))
            {
                db.Open();
                var airports = db.Query<Airport>(Airport.GETALL);
                return (List<Airport>)airports;
            }
        }

        public Airport Get(string code)
        {
            using (var db = new SqlConnection(_connection))
            {
                db.Open();
                var airport = db.QueryFirstOrDefault<Airport>(Airport.GET, new { Code = code });
                return airport;
            }
        }
    }
}
