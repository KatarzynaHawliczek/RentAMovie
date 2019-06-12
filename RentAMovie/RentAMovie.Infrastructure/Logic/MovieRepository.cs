using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using RentAMovie.Infrastructure.Context;
using RentAMovie.Infrastructure.Model;

namespace RentAMovie.Infrastructure.Logic
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContext _movieContext;

        public MovieRepository(MovieContext movieContext)
        {
            _movieContext = movieContext;
        }
        
        public async Task<IEnumerable<Movie>> GetAll()
        {
            var movies = await _movieContext.Movie.ToListAsync();
            return movies;
        }

        public async Task<Movie> GetById(long id)
        {
            var movie = await _movieContext.Movie
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
            
            return movie;
        }
        
        public async Task<IEnumerable<Movie>> GetByTitle(string title)
        {
            var movies = await _movieContext.Movie
                .Where(x => x.Title == title)
                .ToListAsync();
           
            return movies;
        }
        
        public async Task<IEnumerable<Movie>> GetByGenre(string genre)
        {
            var movies = await _movieContext.Movie
                .Where(x => x.Genre == genre)
                .ToListAsync();
           
            return movies;
        }
        
        public async Task<IEnumerable<Movie>> GetByReleaseDate(int releaseDate)
        {
            var movies = await _movieContext.Movie
                .Where(x => x.ReleaseDate == releaseDate)
                .ToListAsync();
           
            return movies;
        }

        public async Task<IEnumerable<Movie>> GetRentedMovies()
        {
            var movies = await _movieContext.Movie
                .Where(x => x.IsRented)
                .ToListAsync();

            return movies;
        }

        public async Task<IEnumerable<Movie>> SortByGenre()
        {
            var movies = await _movieContext.Movie.OrderBy(x => x.Genre).ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Movie>> SortByReleaseDate()
        {
            var movies = await _movieContext.Movie.OrderByDescending(x => x.ReleaseDate).ToListAsync();
            return movies;
        }

        public async Task Add(Movie movie)
        {
            movie.DateOfCreation = DateTime.Now;
            movie.Id = null;
            movie.IsRented = false;
            await _movieContext.Movie.AddAsync(movie);
            await _movieContext.SaveChangesAsync();
        }

        public async Task Update(Movie entity)
        {
            var movieToUpdate = await _movieContext.Movie
                .SingleOrDefaultAsync(x => x.Id == entity.Id);

            if (movieToUpdate != null)
            {
                movieToUpdate.Title = entity.Title;
                movieToUpdate.Director = entity.Director;
                movieToUpdate.Genre = entity.Genre;
                movieToUpdate.ReleaseDate = entity.ReleaseDate;
                movieToUpdate.Price = entity.Price;
                movieToUpdate.Country = entity.Country;
                movieToUpdate.IsRented = entity.IsRented;
                movieToUpdate.DateOfUpdate = DateTime.Now;

                await _movieContext.SaveChangesAsync();
            }
        }

        public async Task Delete(long id)
        {
            var movieToDelete = await _movieContext.Movie.SingleOrDefaultAsync(movie => movie.Id == id);
            if (movieToDelete != null)
            {
                _movieContext.Movie.Remove(movieToDelete);
                await _movieContext.SaveChangesAsync();
            }
        }
    }
}