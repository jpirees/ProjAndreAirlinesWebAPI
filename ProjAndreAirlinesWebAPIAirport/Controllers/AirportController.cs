using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjAndreAirlinesWebAPI.Model;
using ProjAndreAirlinesWebAPI.Services;
using ProjAndreAirlinesWebAPI.Utils;
using ProjAndreAirlinesWebAPIAirport.Services;

namespace ProjAndreAirlinesWebAPIAirport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly AirportService _airportService;

        public AirportController(AirportService airportService)
        {
            _airportService = airportService;
        }

        [HttpGet]
        public ActionResult<List<Airport>> Get() =>
            _airportService.Get();

        [HttpGet("{id:length(24)}", Name = "GetAirport")]
        public ActionResult<Airport> Get(string id)
        {
            var airport = _airportService.Get(id);

            if (airport == null)
                return NotFound(new ResponseAPI(404, "Aeroporto não encontrado."));

            return airport;
        }

        [HttpGet("{iataCode}")]
        public ActionResult<Airport> GetByIataCode(string iataCode)
        {
            var airport = _airportService.GetByIataCode(iataCode);

            if (airport == null)
                return NotFound(new ResponseAPI(404, "Aeroporto não encontrado."));

            return airport;
        }

        [HttpPost]
        public async Task<ActionResult<Airport>> Create(Airport airport)
        {
            if (_airportService.GetByIataCode(airport.IataCode) != null)
                return BadRequest(new ResponseAPI(400, "Aeroporto já cadastrado."));

            var address = await ViaCepService.SearchAddressByZipCode(airport.Address.ZipCode);

            if (address != null)
            {
                address.ZipCode = address.ZipCode.Replace("-", "");
                address.Number = airport.Address.Number;
                airport.Address = address;
            }

            _airportService.Create(airport);
            
            return CreatedAtRoute("GetAirport", new { id = airport.Id.ToString() }, airport);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Airport airportIn)
        {
            var airport = _airportService.Get(id);

            if (airport == null)
                return NotFound(new ResponseAPI(404, "Aeroporto não encontrado."));

            _airportService.Update(id, airportIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var airport = _airportService.Get(id);

            if (airport == null)
                return NotFound(new ResponseAPI(404, "Aeroporto não encontrado."));

            _airportService.Remove(id);

            return NoContent();
        }
    }
}
