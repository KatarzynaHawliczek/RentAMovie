using RentAMovie.Contract.BorrowDto;
using RentAMovie.Infrastructure.Model;

namespace RentAMovie.Core.Services.Mappers
{
    internal static class BorrowMapper
    {
        public static BorrowDto MapBorrowToDto(Borrow borrow)
        {
            return new BorrowDto()
            {
                DateOfBorrow = borrow.DateOfBorrow,
                DateOfReturn = borrow.DateOfReturn,
                NumberOfDays = borrow.NumberOfDays
                //ClientId = borrow.Client.Id,
                //MovieId = borrow.Movie.Id
            };
        }

        public static Borrow MapDtoToBorrow(BorrowDto borrow)
        {
            return new Borrow()
            {
                DateOfBorrow = borrow.DateOfBorrow,
                DateOfReturn = borrow.DateOfReturn,
                NumberOfDays = borrow.NumberOfDays
                //Client = new Client()
                //{
                //    Id = borrow.ClientId
                //},
                //Movie = new Movie()
                //{
                //    Id = borrow.MovieId
                //}
            };
        }
    }
}