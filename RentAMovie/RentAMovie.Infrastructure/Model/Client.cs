using System.Collections.Generic;

namespace RentAMovie.Infrastructure.Model
{
    public class Client : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<Borrow> Borrows { get; set; }
    }
}