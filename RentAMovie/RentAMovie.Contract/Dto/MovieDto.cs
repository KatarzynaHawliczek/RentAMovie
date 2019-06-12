using System;

namespace RentAMovie.Contract.Dto
{
    public class MovieDto : Dto
    {
        public string Title { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public int? ReleaseDate { get; set; }
        public decimal? Price { get; set; }
        public string Country { get; set; }
        public bool? IsRented { get; set; }
    }
}