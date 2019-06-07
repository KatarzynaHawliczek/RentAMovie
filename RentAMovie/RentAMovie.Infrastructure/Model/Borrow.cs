using System;

namespace RentAMovie.Infrastructure.Model
{
    public class Borrow : Entity
    {
        public DateTime DateOfBorrow { get; set; }
        public DateTime DateOfReturn { get; set; }
        public Client Client { get; set; }
        public Movie Movie { get; set; }
    }
}