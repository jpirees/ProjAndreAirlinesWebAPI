using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjAndreAirlinesWebAPI.Model.DTO;
using ProjAndreAirlinesWebAPI.Utils;
using ProjAndreAirlinesWebAPIDapper.Repositories;

namespace ProjAndreAirlinesWebAPIDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportsController : ControllerBase
    {
        private readonly IAirportRepository _airportRepository;

        public AirportsController(AirportRepository airportRepository)
        {
            _airportRepository = airportRepository;
        }

        [HttpGet]
        public ActionResult<List<Airport>> Get()
        {
            try
            {
                var airports = _airportRepository.GetAll();
                return Ok(airports);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpGet("{code}", Name = "GetAirport")]
        public ActionResult<Airport> Get(string code)
        {
            var airport = _airportRepository.Get(code);

            if (airport == null)
                return NotFound(new ResponseAPI(404, "Aeroporto não encontrado."));

            return StatusCode(200, airport);
        }

        [HttpPost]
        public ActionResult<Airport> Create(Airport airport)
        {
            var airportExists = _airportRepository.Get(airport.Code);

            if (airportExists != null)
                return BadRequest(new ResponseAPI(400, "Aeroporto já cadastrado."));

            _airportRepository.Add(airport);

            return CreatedAtRoute("GetAirport", new { code = airport.Code }, airport);
        }
    }
}
