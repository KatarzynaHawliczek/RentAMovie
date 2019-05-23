using System;

namespace RentAMovie.Contract.MovieDto
{
    public class MovieDto
    {
        public string Title { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public byte[] Image { get; set; }
        public string Country { get; set; }
        public bool IsRented { get; set; }
        //public long ClientId { get; set; }
        //public long BorrowId { get; set; }
    }
}