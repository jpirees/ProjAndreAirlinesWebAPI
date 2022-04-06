using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjAndreAirlinesWebAPI.Model;
using ProjAndreAirlinesWebAPIFlight.Services;

namespace ProjAndreAirlinesWebAPIFlight.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly FlightService _flightService;

        public FlightController(FlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        public ActionResult<List<Flight>> Get() =>
            _flightService.Get();

        [HttpGet("{id:length(24)}", Name = "GetFlight")]
        public ActionResult<Flight> Get(string id)
        {
            var flight = _flightService.Get(id);

            if (flight == null)
                return NotFound("Vôo não programado.");

            return flight;
        }

        [HttpGet("ico={iataCodeOrigin}&icd={iataCodeDestination}&bt={boardingTime}")]
        public ActionResult<Flight> GetFlight(string iataCodeOrigin, string iataCodeDestination, DateTime boardingTime)
        {
            var flight = _flightService.GetFlight(iataCodeOrigin, iataCodeDestination, boardingTime);

            if (flight == null)
                return NotFound("Vôo não programado.");

            return flight;
        }

        [HttpPost]
        public async Task<ActionResult<Flight>> Create(Flight flight)
        {
            AircraftServiceAPI _aircraftServiceAPI = new();
            AirportServiceAPI _airportServiceAPI = new();

            try
            {
                var aircraft = await _aircraftServiceAPI.GetAircraftAsync(flight.Aircraft.RegistrationCode);

                if (aircraft == null)
                    return NotFound("Aeronave não encontrada");

                flight.Aircraft = aircraft;
            }
            catch (Exception)
            {
                throw new Exception("Falha ao realizar busca de aeronaves por matrícula.");
            }

            try
            {
                var airport = await _airportServiceAPI.GetAirportAsync(flight.Origin.IataCode);

                if (airport == null)
                    return NotFound("Aeroporto não econtrado");

                flight.Origin = airport;
            }
            catch (Exception)
            {
                throw new Exception("Falha ao realizar busca de aeroporto de origem por código IATA.");
            }

            try
            {
                var airport = await _airportServiceAPI.GetAirportAsync(flight.Destination.IataCode);

                if (airport == null)
                    return NotFound("Aeroporto não econtrado");

                flight.Destination = airport;
            }
            catch (Exception)
            {
                throw new Exception("Falha ao realizar busca de aeroporto de destino por código IATA.");
            }

            _flightService.Create(flight);

            return CreatedAtRoute("GetFlight", new { id = flight.Id.ToString() }, flight);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Flight flightIn)
        {
            var flight = _flightService.Get(id);

            if (flight == null)
                return NotFound("Vôo não programado.");

            _flightService.Update(id, flightIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var flight = _flightService.Get(id);

            if (flight == null)
                return NotFound("Vôo não programado.");

            _flightService.Remove(id);

            return NoContent();
        }
    }
}
