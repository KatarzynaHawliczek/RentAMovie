using System;
using System.Collections.Generic;

namespace RentAMovie.Infrastructure.Model
{
    public class Movie : Entity
    {
        public string Title { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public string Country { get; set; }
        public bool IsRented { get; set; }
        public IEnumerable<Borrow> Borrows { get; set; }
    }
}