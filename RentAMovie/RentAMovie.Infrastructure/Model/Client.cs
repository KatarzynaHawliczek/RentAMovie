using System.Collections.Generic;

namespace RentAMovie.Infrastructure.Model
{
    public class Client : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }
        public List<Movie> Movies { get; set; }
        public Borrow Borrow { get; set; }
    }
}