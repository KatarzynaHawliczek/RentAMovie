using RentAMovie.Contract.Dto;
using RentAMovie.Infrastructure.Model;

namespace RentAMovie.Core.Services.Mappers
{
    internal static class MovieMapper
    {
        public static MovieDto MapMovieToDto(Movie movie)
        {
            return new MovieDto()
            {
                Id = movie.Id,
                Title = movie.Title,
                Director = movie.Director,
                Genre = movie.Genre,
                ReleaseDate = movie.ReleaseDate,
                Price = movie.Price,
                Country = movie.Country,
                IsRented = movie.IsRented
            };
        }

        public static Movie MapDtoToMovie(MovieDto movie)
        {
            return new Movie()
            {
                Id = movie.Id.GetValueOrDefault(),
                Title = movie.Title,
                Director = movie.Director,
                Genre = movie.Genre,
                ReleaseDate = movie.ReleaseDate.GetValueOrDefault(),
                Price = movie.Price.GetValueOrDefault(),
                Country = movie.Country,
                IsRented = movie.IsRented.GetValueOrDefault()
            };
        }
    }
}