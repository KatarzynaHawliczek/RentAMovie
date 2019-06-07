using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentAMovie.Contract.Dto;
using RentAMovie.Core.Services;

namespace RentAMovie.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("GetAddress/{Id}")]
        public async Task<IActionResult> GetAddressById(long id)
        {
            try
            {
                var address = await _addressService.GetById(id);
                return Ok(address);
            }
            catch (NullReferenceException e)
            {
                return NotFound($"Can't find address with id = {id}");
            }
        }

        [HttpGet("GetAllAddresses")]
        public async Task<IActionResult> GetAllAddresses()
        {
            var addresses = await _addressService.GetAll();
            return Ok(addresses);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] AddressDto address)
        {
            if (address == null)
            {
                return BadRequest();
            }

            await _addressService.Add(address);
            return Created("Created new address", address);
        }

        [HttpPut("UpdateAddress")]
        public async Task<IActionResult> UpdateAddress([FromBody] AddressDto address)
        {
            if (address == null)
            {
                return BadRequest();
            }

            await _addressService.Update(address);
            return Ok($"Updated address with id = {address.Id}");
        }

        [HttpDelete("DeleteAddress/{id}")]
        public async Task<IActionResult> DeleteAddress(long id)
        {
            await _addressService.Delete(id);
            return Ok($"Address with id = {id} deleted");
        }
    }
}

