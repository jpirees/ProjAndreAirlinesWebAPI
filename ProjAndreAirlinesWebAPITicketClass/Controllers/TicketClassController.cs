using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjAndreAirlinesWebAPI.Model;
using ProjAndreAirlinesWebAPITicketClass.Services;

namespace ProjAndreAirlinesWebAPITicketClass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketClassController : ControllerBase
    {
        private readonly TicketClassService _ticketClassService;

        public TicketClassController(TicketClassService ticketClassService)
        {
            _ticketClassService = ticketClassService;
        }

        [HttpGet]
        public ActionResult<List<TicketClass>> Get() =>
            _ticketClassService.Get();

        [HttpGet("{id:length(24)}", Name = "GetTicketClass")]
        public ActionResult<TicketClass> Get(string id)
        {
            var ticketClass = _ticketClassService.Get(id);

            if (ticketClass == null)
                return NotFound("Classe não encontrada.");

            return ticketClass;
        }

        [HttpGet("{description}")]
        public ActionResult<TicketClass> GetByDescription(string description)
        {
            var ticketClass = _ticketClassService.GetByDescription(description);

            if (ticketClass == null)
                return NotFound("Classe não encontrada.");

            return ticketClass;
        }

        [HttpPost]
        public ActionResult<TicketClass> Create(TicketClass ticketClass)
        {
            var ticketClassExist = _ticketClassService.GetByDescription(ticketClass.Description);

            if (ticketClassExist != null)
                return BadRequest("Classe já cadastrada.");

            _ticketClassService.Create(ticketClass);

            return CreatedAtRoute("GetTicketClass", new { id = ticketClass.Id.ToString() }, ticketClass);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, TicketClass ticketClassIn)
        {
            var ticketClass = _ticketClassService.Get(id);

            if (ticketClass == null)
                return NotFound("Classe não encontrada.");

            _ticketClassService.Update(id, ticketClassIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var ticketClass = _ticketClassService.Get(id);

            if (ticketClass == null)
                return NotFound("Classe não encontrada.");

            _ticketClassService.Remove(id);

            return NoContent();
        }
    }
}
