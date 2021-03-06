using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentAMovie.Contract.Dto;
using RentAMovie.Core.Services.Mappers;
using RentAMovie.Infrastructure.Logic;
using RentAMovie.Infrastructure.Model;

namespace RentAMovie.Core.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _iMovieRepository;

        public MovieService(IMovieRepository iMovieRepository)
        {
            _iMovieRepository = iMovieRepository;
        }

        public async Task<IEnumerable<MovieDto>> GetAll()
        {
            var movies = await _iMovieRepository.GetAll();
            return movies
                .Select(MovieMapper.MapMovieToDto)
                .ToList();
        }

        public async Task<MovieDto> GetById(long id)
        {
            var movie = await _iMovieRepository.GetById(id);
            return MovieMapper.MapMovieToDto(movie);
        }
        
        public async Task<IEnumerable<MovieDto>> GetByTitle(string title)
        {
            var movies = await _iMovieRepository.GetByTitle(title);
            return movies
                .Select(MovieMapper.MapMovieToDto)
                .ToList();
        }

        public async Task<IEnumerable<MovieDto>> GetByGenre(string genre)
        {
            var movies = await _iMovieRepository.GetByGenre(genre);
            return movies
                .Select(MovieMapper.MapMovieToDto)
                .ToList();
        }

        public async Task<IEnumerable<MovieDto>> GetByReleaseDate(int releaseDate)
        {
            var movies = await _iMovieRepository.GetByReleaseDate(releaseDate);
            return movies
                .Select(MovieMapper.MapMovieToDto)
                .ToList();
        }

        public async Task<IEnumerable<MovieDto>> GetRentedMovies()
        {
            var movies = await _iMovieRepository.GetRentedMovies();
            return movies
                .Select(MovieMapper.MapMovieToDto)
                .ToList();
        }

        public async Task<IEnumerable<MovieDto>> SortByGenre()
        {
            var movies = await _iMovieRepository.SortByGenre();
            return movies
                .Select(MovieMapper.MapMovieToDto)
                .ToList();
        }

        public async Task<IEnumerable<MovieDto>> SortByReleaseDate()
        {
            var movies = await _iMovieRepository.SortByReleaseDate();
            return movies
                .Select(MovieMapper.MapMovieToDto)
                .ToList();
        }

        public async Task Add(MovieDto movie)
        {
            await _iMovieRepository.Add(MovieMapper.MapDtoToMovie(movie));
        }

        public async Task Update(MovieDto entity)
        {
            await _iMovieRepository.Update(MovieMapper.MapDtoToMovie(entity));
        }

        public async Task Delete(long id)
        {
            await _iMovieRepository.Delete(id);
        }
    }
}