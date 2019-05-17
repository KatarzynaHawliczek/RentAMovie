using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            movies.ForEach(x => { _movieContext.Entry(x).Reference(y => y.Client).LoadAsync(); });
            movies.ForEach(x => { _movieContext.Entry(x).Reference(y => y.Borrow).LoadAsync(); });
            return movies;
        }

        public async Task<Movie> GetById(long id)
        {
            var movie = await _movieContext.Movie
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
            await _movieContext.Entry(movie).Reference(x => x.Client).LoadAsync();
            await _movieContext.Entry(movie).Reference(x => x.Borrow).LoadAsync();
            return movie;
        }

        public async Task Add(Movie movie)
        {
            movie.DateOfCreation = DateTime.Now;
            await _movieContext.Movie
                .Include(x => x.Client)
                .Include(x => x.Borrow)
                .FirstAsync();
            await _movieContext.Movie.AddAsync(movie);
            await _movieContext.SaveChangesAsync();
        }

        public async Task Update(Movie entity)
        {
            var movieToUpdate = await _movieContext.Movie
                .Include(x => x.Client)
                .Include(x => x.Borrow)
                .SingleOrDefaultAsync(x => x.Id == entity.Id);

            if (movieToUpdate != null)
            {
                movieToUpdate.Title = entity.Title;
                movieToUpdate.Director = entity.Director;
                movieToUpdate.Genre = entity.Genre;
                movieToUpdate.ReleaseDate = entity.ReleaseDate;
                movieToUpdate.Price = entity.Price;
                movieToUpdate.Image = entity.Image;
                movieToUpdate.Country = entity.Country;
                movieToUpdate.IsRented = entity.IsRented;
                movieToUpdate.Client = entity.Client;
                movieToUpdate.Borrow = entity.Borrow;
                movieToUpdate.DateOfUpdate = DateTime.Now;

                if (entity.Client != null && movieToUpdate.Client != null)
                {
                    entity.Client.Id = movieToUpdate.Client.Id;
                    _movieContext.Entry(movieToUpdate.Client).CurrentValues.SetValues(entity.Client);
                }

                if (entity.Borrow != null && movieToUpdate.Borrow != null)
                {
                    entity.Borrow.Id = movieToUpdate.Borrow.Id;
                    _movieContext.Entry(movieToUpdate.Borrow).CurrentValues.SetValues(entity.Borrow);
                }

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