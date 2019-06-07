using System;

namespace RentAMovie.Contract.Dto
{
    public class BorrowDto : BaseDto
    {
        public DateTime DateOfBorrow { get; set; }
        public DateTime DateOfReturn { get; set; }
    }
}