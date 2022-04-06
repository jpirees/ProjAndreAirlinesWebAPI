using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProjAndreAirlinesWebAPI.Model;
using ProjAndreAirlinesWebAPIAircraft.Services;

namespace ProjAndreAirlinesWebAPIAircraft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftController : ControllerBase
    {
        private readonly AircraftService _aircraftService;

        public AircraftController(AircraftService aircraftService)
        {
            _aircraftService = aircraftService;
        }

        [HttpGet]
        public ActionResult<List<Aircraft>> Get() =>
            _aircraftService.Get();

        [HttpGet("{id:length(24)}", Name = "GetAircraft")]
        public ActionResult<Aircraft> Get(string id)
        {
            var aircraft = _aircraftService.Get(id);

            if (aircraft == null)
                return NotFound("Aeronave não encontrada.");

            return aircraft;
        }

        [HttpGet("{RegistrationCode}")]
        public ActionResult<Aircraft> GetByRegistrationCode(string registrationCode)
        {
            var aircraft = _aircraftService.GetByRegistrationCode(registrationCode);

            if (aircraft == null)
                return NotFound("Aeronave não encontrada.");

            return aircraft;
        }

        [HttpPost]
        public ActionResult<Aircraft> Create(Aircraft aircraft)
        {
            _aircraftService.Create(aircraft);

            return CreatedAtRoute("GetAircraft", new { id = aircraft.Id.ToString() }, aircraft);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Aircraft aircraftIn)
        {
            var aircraft = _aircraftService.Get(id);

            if (aircraft == null)
                return NotFound("Aeronave não encontrada.");

            _aircraftService.Update(id, aircraftIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var aircraft = _aircraftService.Get(id);

            if (aircraft == null)
                return NotFound("Aeronave não encontrada.");

            _aircraftService.Remove(id);

            return NoContent();
        }

    }
}
