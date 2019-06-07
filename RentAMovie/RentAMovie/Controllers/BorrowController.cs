using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentAMovie.Contract.Dto;
using RentAMovie.Core.Services;

namespace RentAMovie.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BorrowController : ControllerBase
    {
        private readonly IBorrowService _borrowService;

        public BorrowController(IBorrowService borrowService)
        {
            _borrowService = borrowService;
        }

        [HttpGet("GetBorrow/{Id}")]
        public async Task<IActionResult> GetBorrowById(long id)
        {
            try
            {
                var borrow = await _borrowService.GetById(id);
                return Ok(borrow);
            }
            catch (NullReferenceException e)
            {
                return NotFound($"Can't find borrow with id = {id}");
            }
        }

        [HttpGet("GetAllBorrows")]
        public async Task<IActionResult> GetAllBorrows()
        {
            var borrows = await _borrowService.GetAll();
            return Ok(borrows);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBorrow([FromBody] BorrowDto borrow)
        {
            if (borrow == null)
            {
                return BadRequest();
            }

            await _borrowService.Add(borrow);
            return Created("Created new borrow", borrow);
        }

        [HttpPut("UpdateBorrow")]
        public async Task<IActionResult> UpdateBorrow([FromBody] BorrowDto borrow)
        {
            if (borrow == null)
            {
                return BadRequest();
            }

            await _borrowService.Update(borrow);
            return Ok($"Updated borrow with id = {borrow.Id}");
        }

        [HttpDelete("DeleteBorrow/{id}")]
        public async Task<IActionResult> DeleteBorrow(long id)
        {
            await _borrowService.Delete(id);
            return Ok($"Borrow with id = {id} deleted");
        }
    }
}

