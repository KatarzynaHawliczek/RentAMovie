using System;

namespace RentAMovie.Contract.BorrowDto
{
    public class BorrowDto
    {
        public DateTime DateOfBorrow { get; set; }
        public DateTime DateOfReturn { get; set; }
        public long NumberOfDays { get; set; }
        //public long ClientId { get; set; }
        //public long MovieId { get; set; }
    }
}