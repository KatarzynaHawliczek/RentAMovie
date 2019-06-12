using System;

namespace RentAMovie.Contract.Dto
{
    public class BorrowDto : Dto
    {
        public DateTime? DateOfBorrow { get; set; }
        public DateTime? DateOfReturn { get; set; }
        public long? ClientId { get; set; }
        public long? MovieId { get; set; }
    }
}