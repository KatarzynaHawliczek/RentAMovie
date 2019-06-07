using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentAMovie.Contract.Dto;
using RentAMovie.Core.Services;

namespace RentAMovie.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet("GetClient/{Id}")]
        public async Task<IActionResult> GetClientById(long id)
        {
            try
            {
                var client = await _clientService.GetById(id);
                return Ok(client);
            }
            catch (NullReferenceException e)
            {
                return NotFound($"Can't find client with id = {id}");
            }
        }

        [HttpGet("GetAllClients")]
        public async Task<IActionResult> GetAllClients()
        {
            var clients = await _clientService.GetAll();
            return Ok(clients);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] ClientDto client)
        {
            if (client == null)
            {
                return BadRequest();
            }

            await _clientService.Add(client);
            return Created("Created new client", client);
        }

        [HttpPut("UpdateClient")]
        public async Task<IActionResult> UpdateClient([FromBody] ClientDto client)
        {
            if (client == null)
            {
                return BadRequest();
            }

            await _clientService.Update(client);
            return Ok($"Updated client with id = {client.Id}");
        }

        [HttpDelete("DeleteClient/{id}")]
        public async Task<IActionResult> DeleteClient(long id)
        {
            await _clientService.Delete(id);
            return Ok($"Movie with id = {id} deleted");
        }
    }
}