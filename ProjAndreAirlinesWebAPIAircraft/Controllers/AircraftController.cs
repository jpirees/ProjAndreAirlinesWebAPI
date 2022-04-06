using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjAndreAirlinesWebAPI.Model;
using ProjAndreAirlinesWebAPI.Utils;
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
                return NotFound(new ResponseAPI(404, "Aeronave não encontrada."));

            return aircraft;
        }

        [HttpGet("{registrationCode}")]
        public ActionResult<Aircraft> GetByRegistrationCode(string registrationCode)
        {
            var aircraft = _aircraftService.GetByRegistrationCode(registrationCode);

            if (aircraft == null)
                return NotFound(new ResponseAPI(404, "Aeronave não encontrada."));
            
            return aircraft;
        }

        [HttpPost]
        public ActionResult<Aircraft> Create(Aircraft aircraft)
        {
            var aircraftExists = _aircraftService.GetByRegistrationCode(aircraft.RegistrationCode);

            if (aircraftExists != null)
                return BadRequest(new ResponseAPI(400, "Aeronave já cadastrada."));

            _aircraftService.Create(aircraft);

            return CreatedAtRoute("GetAircraft", new { id = aircraft.Id.ToString() }, aircraft);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Aircraft aircraftIn)
        {
            var aircraft = _aircraftService.Get(id);

            if (aircraft == null)
                return NotFound(new ResponseAPI(404, "Aeronave não encontrada."));

            _aircraftService.Update(id, aircraftIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var aircraft = _aircraftService.Get(id);

            if (aircraft == null)
                return NotFound(new ResponseAPI(404, "Aeronave não encontrada."));

            _aircraftService.Remove(id);

            return NoContent();
        }

    }
}
