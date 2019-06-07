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
            //movies.ForEach(x => { _movieContext.Entry(x).Reference(y => y.Client).LoadAsync(); });
            movies.ForEach(x => { _movieContext.Entry(x).Reference(y => y.Borrows).LoadAsync(); });
            return movies;
        }

        public async Task<Movie> GetById(long id)
        {
            var movie = await _movieContext.Movie
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
            try
            {
                await _movieContext.Entry(movie).Reference(x => x.Borrows).LoadAsync();
            }
            catch (ArgumentException e)
            {
                return null;
            }
            return movie;
        }

        public async Task Add(Movie movie)
        {
            movie.DateOfCreation = DateTime.Now;
            await _movieContext.Movie
                .Include(x => x.Borrows)
                .FirstAsync();
            await _movieContext.Movie.AddAsync(movie);
            await _movieContext.SaveChangesAsync();
        }

        public async Task Update(Movie entity)
        {
            var movieToUpdate = await _movieContext.Movie
                .Include(x => x.Borrows)
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
                movieToUpdate.Borrows = entity.Borrows;
                movieToUpdate.DateOfUpdate = DateTime.Now;

                if (entity.Borrows != null && movieToUpdate.Borrows != null)
                {
                    var borrowsToUpdate = movieToUpdate.Borrows.ToList();
                    foreach (var borrow in borrowsToUpdate)
                    {
                        foreach (var entityBorrow in entity.Borrows)
                        {
                            if (borrow.Id == entityBorrow.Id)
                            {
                                _movieContext.Entry(borrowsToUpdate).CurrentValues.SetValues(entity.Borrows);
                            }
                        }
                    }
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