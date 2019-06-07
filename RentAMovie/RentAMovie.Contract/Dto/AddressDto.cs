namespace RentAMovie.Contract.Dto
{
    public class AddressDto : BaseDto
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }
}