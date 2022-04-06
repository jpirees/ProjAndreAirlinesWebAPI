using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjAndreAirlinesWebAPI.Model;
using ProjAndreAirlinesWebAPI.Utils;
using ProjAndreAirlinesWebAPIBasePrice.Services;

namespace ProjAndreAirlinesWebAPIBasePrice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasePriceController : ControllerBase
    {
        private readonly BasePriceService _basePriceService;

        public BasePriceController(BasePriceService basePriceService)
        {
            _basePriceService = basePriceService;
        }

        [HttpGet]
        public ActionResult<List<BasePrice>> Get() =>
            _basePriceService.Get();

        [HttpGet("{id:length(24)}", Name = "GetBasePrice")]
        public ActionResult<BasePrice> Get(string id)
        {
            var basePrice = _basePriceService.Get(id);

            if (basePrice == null)
                return NotFound(new ResponseAPI(404, "Preço base não cadastrado."));

            return basePrice;
        }

        [HttpGet("{iataCodeOrigin}/{iataCodeDestination}")]
        public ActionResult<BasePrice> GetByAirports(string iataCodeOrigin, string iataCodeDestination)
        {
            var basePrice = _basePriceService.GetByAirports(iataCodeOrigin, iataCodeDestination);

            if (basePrice == null)
                return NotFound(new ResponseAPI(404, "Preço base não cadastrado."));

            return basePrice;
        }

        [HttpPost]
        public async Task<ActionResult<BasePrice>> Create(BasePrice basePrice)
        {
            var airportOrigin = await _basePriceService.GetAiportByIataCode(basePrice.Origin.IataCode);

            if (airportOrigin == null)
                return NotFound(new ResponseAPI(404, "Aeroporto de origem não encontrado."));


            var airportDestination = await _basePriceService.GetAiportByIataCode(basePrice.Origin.IataCode);

            if (airportDestination == null)
                return NotFound(new ResponseAPI(404, "Aeroporto de destino não encotrado."));


            if (airportOrigin.IataCode.Equals(airportDestination.IataCode))
                return BadRequest(new ResponseAPI(400, "Aeroporto de destino não pode ser o mesmo de origem."));
            else
            {
                basePrice.Origin = airportOrigin;
                basePrice.Destination = airportDestination;
            }

            _basePriceService.Create(basePrice);

            return CreatedAtRoute("GetBasePrice", new { id = basePrice.Id.ToString() }, basePrice);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, BasePrice basePriceIn)
        {
            var basePrice = _basePriceService.Get(id);

            if (basePrice == null)
                return NotFound(new ResponseAPI(404, "Preço base não cadastrado."));

            _basePriceService.Update(id, basePriceIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var basePrice = _basePriceService.Get(id);

            if (basePrice == null)
                return NotFound(new ResponseAPI(404, "Preço base não cadastrado."));

            _basePriceService.Remove(id);

            return NoContent();
        }
    }
}
