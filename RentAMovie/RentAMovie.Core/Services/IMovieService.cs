using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentAMovie.Contract.Dto;

namespace RentAMovie.Core.Services
{
    public interface IMovieService : IService<MovieDto>
    {
        Task<IEnumerable<MovieDto>> GetByTitle(string title);
        Task<IEnumerable<MovieDto>> GetByGenre(string genre);
        Task<IEnumerable<MovieDto>> GetByReleaseDate(DateTime releaseDate);
    }
}