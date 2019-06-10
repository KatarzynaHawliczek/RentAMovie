using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentAMovie.Infrastructure.Model;

namespace RentAMovie.Infrastructure.Logic
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetByTitle(string title);
        Task<IEnumerable<Movie>> GetByGenre(string genre);
        Task<IEnumerable<Movie>> GetByReleaseDate(DateTime releaseDate);
    }
}