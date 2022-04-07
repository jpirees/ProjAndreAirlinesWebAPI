using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjAndreAirlinesWebAPI.Model;
using ProjAndreAirlinesWebAPI.Utils;
using ProjAndreAirlinesWebAPIUser.Services;

namespace ProjAndreAirlinesWebAPIUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> Get() =>
            await _userService.Get();

        [HttpGet("{id:length(24)}", Name = "GetUser")]
        public async Task<ActionResult<User>> Get(string id) =>
            await _userService.Get(id);

        [HttpGet("{cpf}/Profile")]
        public async Task<ActionResult<User>> GetDocument(string cpf)
        {
            var user = await _userService.GetUserByDocument(cpf);

            if (user == null)
                return NotFound(new ResponseAPI(404, "Usuário nã encontrado"));

            return user;
        }

        [HttpGet("{username}/Access")]
        public async Task<ActionResult<User>> GetUsername(string username)
        {
            var user = await _userService.GetUserByUsername(username);

            if (user == null)
                return NotFound(new ResponseAPI(404, "Usuário nã encontrado"));


            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
        {
            var userDocument = await _userService.GetUserByDocument(user.Cpf) 
                            ?? await _userService.GetUserByUsername(user.Username);

            if (userDocument != null)
                return BadRequest(new ResponseAPI(400, "Usuário já cadastrado."));

            await _userService.Create(user);

            return CreatedAtRoute("GetUser", new { id = user.Id }, user);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, User user)
        {
            await _userService.Update(id, user);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userService.Get(id);

            if (user == null)
                return BadRequest(new ResponseAPI(404, "Usuário não encontrado."));

            await _userService.Remove(id);

            return NoContent();
        }
    }
}
