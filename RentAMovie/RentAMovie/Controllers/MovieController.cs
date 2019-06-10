using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentAMovie.Contract.Dto;
using RentAMovie.Core.Services;

namespace RentAMovie.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("GetMovie/{id}")]
        public async Task<IActionResult> GetMovieById(long id)
        {
            try
            {
                var movie = await _movieService.GetById(id);
                return Ok(movie);
            }
            catch (NullReferenceException e)
            {
                return NotFound($"Movie with id = {id} not found.");
            }
        }
        
        [HttpGet("GetMovieByTitle/{title}")]
        public async Task<IActionResult> GetMovieByTitle(string title)
        {
            try
            {
                var movie = await _movieService.GetByTitle(title);
                return Ok(movie);
            }
            catch (NullReferenceException e)
            {
                return NotFound($"Movie with title = {title} not found.");
            }
        }
        
        [HttpGet("GetMovieByGenre/{genre}")]
        public async Task<IActionResult> GetMovieById(string genre)
        {
            try
            {
                var movie = await _movieService.GetByGenre(genre);
                return Ok(movie);
            }
            catch (NullReferenceException e)
            {
                return NotFound($"Movie with genre = {genre} not found.");
            }
        }
        
        [HttpGet("GetMovieByReleaseDate/{releaseDate}")]
        public async Task<IActionResult> GetMovieByReleaseDate(DateTime releaseDate)
        {
            try
            {
                var movie = await _movieService.GetByReleaseDate(releaseDate);
                return Ok(movie);
            }
            catch (NullReferenceException e)
            {
                return NotFound($"Movie with releaseDate = {releaseDate} not found.");
            }
        }

        [HttpGet("GetAllMovies")]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _movieService.GetAll();
            return Ok(movies);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromBody] MovieDto movie)
        {
            if (movie == null)
            {
                return BadRequest();
            }

            await _movieService.Add(movie);
            return Created("Created new movie.", movie);
        }

        [HttpPut("UpdateMovie")]
        public async Task<IActionResult> UpdateMovie([FromBody] MovieDto movie)
        {
            if (movie == null)
            {
                return BadRequest();
            }

            await _movieService.Update(movie);
            return Ok($"Movie with id = {movie.Id} updated.");
        }

        [HttpDelete("DeleteMovie/{id}")]
        public async Task<IActionResult> DeleteMovie(long id)
        {
            await _movieService.Delete(id);
            return Ok($"Movie with id = {id} deleted.");
        }
    }
}