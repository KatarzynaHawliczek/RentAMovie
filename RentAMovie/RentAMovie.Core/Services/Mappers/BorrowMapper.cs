using RentAMovie.Contract.Dto;
using RentAMovie.Infrastructure.Model;

namespace RentAMovie.Core.Services.Mappers
{
    internal static class BorrowMapper
    {
        public static BorrowDto MapBorrowToDto(Borrow borrow)
        {
            return new BorrowDto()
            {
                Id = borrow.Id,
                DateOfBorrow = borrow.DateOfBorrow,
                DateOfReturn = borrow.DateOfReturn
            };
        }

        public static Borrow MapDtoToBorrow(BorrowDto borrow)
        {
            return new Borrow()
            {
                Id = borrow.Id,
                DateOfBorrow = borrow.DateOfBorrow,
                DateOfReturn = borrow.DateOfReturn
            };
        }
    }
}