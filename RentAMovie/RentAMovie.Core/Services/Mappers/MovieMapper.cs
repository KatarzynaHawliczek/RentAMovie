using RentAMovie.Contract.MovieDto;
using RentAMovie.Infrastructure.Model;

namespace RentAMovie.Core.Services.Mappers
{
    internal static class MovieMapper
    {
        public static MovieDto MapMovieToDto(Movie movie)
        {
            return new MovieDto()
            {
                Title = movie.Title,
                Director = movie.Director,
                Genre = movie.Genre,
                ReleaseDate = movie.ReleaseDate,
                Price = movie.Price,
                Image = movie.Image,
                Country = movie.Country,
                IsRented = movie.IsRented
                //ClientId = movie.Client.Id,
                //BorrowId = movie.Borrow.Id
            };
        }

        public static Movie MapDtoToMovie(MovieDto movie)
        {
            return new Movie()
            {
                Title = movie.Title,
                Director = movie.Director,
                Genre = movie.Genre,
                ReleaseDate = movie.ReleaseDate,
                Price = movie.Price,
                Image = movie.Image,
                Country = movie.Country,
                IsRented = movie.IsRented
                //Client = new Client()
                //{
                //    Id = movie.ClientId
                //},
                //Borrow = new Borrow()
                //{
                //    Id = movie.BorrowId
                //}
            };
        }
    }
}