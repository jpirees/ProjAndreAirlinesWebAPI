using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjAndreAirlinesWebAPI.Model;
using ProjAndreAirlinesWebAPI.Utils;
using ProjAndreAirlinesWebAPITicket.Services;

namespace ProjAndreAirlinesWebAPITicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly TicketService _ticketService;

        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public ActionResult<List<Ticket>> Get() =>
            _ticketService.Get();

        [HttpGet("{id:length(24)}", Name = "GetTicket")]
        public ActionResult<Ticket> Get(string id)
        {
            var ticket = _ticketService.Get(id);

            if (ticket == null)
                return NotFound(new ResponseAPI(404, "Reserva não encontrada."));

            return ticket;
        }

        [HttpPost]
        public async Task<ActionResult<Ticket>> Create(Ticket ticket)
        {
            FlightServiceAPI _flightServiceAPI = new();
            BasePriceServiceAPI _basePriceServiceAPI = new();
            TicketClassServiceAPI _ticketClassServiceAPI = new();


            var flight = await _flightServiceAPI.GetFlightAsync(ticket.Flight);

            if (flight == null)
                return NotFound(new ResponseAPI(404, "Vôo não encontrado."));

            var basePrice = await _basePriceServiceAPI.GetBasePriceAsync(ticket.Flight);

            if (basePrice == null)
                return NotFound(new ResponseAPI(404, "Preço base não encontrado."));

            var ticketClass = await _ticketClassServiceAPI.GetTicketClassAsync(ticket.TicketClass.Description);

            if (ticketClass == null)
                return NotFound(new ResponseAPI(404, "Preço da classe não encontrado."));

            ticket.Flight = flight;
            ticket.BasePrice = basePrice;
            ticket.TicketClass = ticketClass;

            ticket.TotalPrice = (basePrice.Price + ticketClass.Price) * (1 - (ticket.DiscountPercentage / 100));

            _ticketService.Create(ticket);

            return CreatedAtRoute("GetTicket", new { id = ticket.Id.ToString() }, ticket);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Ticket ticketIn)
        {
            var ticket = _ticketService.Get(id);

            if (ticket == null)
                return NotFound(new ResponseAPI(404, "Reserva não encontrada."));

            _ticketService.Update(id, ticketIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var ticket = _ticketService.Get(id);

            if (ticket == null)
                return NotFound(new ResponseAPI(404, "Reserva não encontrada."));

            _ticketService.Remove(id);

            return NoContent();
        }
    }
}
