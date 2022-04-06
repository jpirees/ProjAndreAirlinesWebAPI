using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjAndreAirlinesWebAPI.Model;
using ProjAndreAirlinesWebAPI.Services;
using ProjAndreAirlinesWebAPIPassenger.Services;

namespace ProjAndreAirlinesWebAPIPassenger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly PassengerService _passengerService;

        public PassengerController(PassengerService passengerService)
        {
            _passengerService = passengerService;
        }

        [HttpGet]
        public ActionResult<List<Passenger>> Get() =>
            _passengerService.Get();

        [HttpGet("{id:length(24)}", Name = "GetPassenger")]
        public ActionResult<Passenger> Get(string id)
        {
            var passenger = _passengerService.Get(id);

            if (passenger == null)
                return NotFound("Passageiro não encontrado.");

            return passenger;
        }

        [HttpGet("{cpf}")]
        public ActionResult<Passenger> GetByCpf(string cpf)
        {
            var passenger = _passengerService.GetByCpf(cpf);

            if (passenger == null)
                return NotFound("Passageiro não encontrado.");

            return passenger;
        }

        [HttpPost] //CreateAsync
        public async Task<ActionResult<Passenger>> Create(Passenger passenger)
        {
            passenger.Cpf = passenger.Cpf.Replace(".", "").Replace("-", "");

            var cpfIsValid = PassengerCpfIsValid(passenger.Cpf);

            if (!cpfIsValid)
                return BadRequest("CPF inválido.");

            var passengerExist = _passengerService.GetByCpf(passenger.Cpf);

            if (passengerExist != null)
                return Conflict("Usuário já cadastrado.");

            var address = await ViaCepService.SearchAddressByCep(passenger.Address.Cep);

            if (address != null)
            {
                address.Cep = address.Cep.Replace("-", "");
                address.Number = passenger.Address.Number;
                passenger.Address = address;
            }

            _passengerService.Create(passenger);

            return CreatedAtRoute("GetPassenger", new { id = passenger.Id.ToString() }, passenger);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Passenger passengerIn)
        {
            var passenger = _passengerService.Get(id);

            if (passenger == null)
                return NotFound("Passageiro não encontrado.");

            _passengerService.Update(id, passengerIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var passenger = _passengerService.Get(id);

            if (passenger == null)
                return NotFound("Passageiro não encontrado.");

            _passengerService.Remove(id);

            return NoContent();
        }

        private static bool PassengerCpfIsValid(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf[..9];
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf += digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito += resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}
