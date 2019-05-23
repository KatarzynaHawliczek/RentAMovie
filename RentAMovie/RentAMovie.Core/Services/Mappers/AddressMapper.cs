using RentAMovie.Contract.AddressDto;
using RentAMovie.Infrastructure.Model;

namespace RentAMovie.Core.Services.Mappers
{
    internal static class AddressMapper
    {
        public static AddressDto MapAddressToDto(Address address)
        {
            return new AddressDto()
            {
                Street = address.Street,
                City = address.City,
                ZipCode = address.ZipCode
            };
        }

        public static Address MapDtoToAddress(AddressDto address)
        {
            return new Address()
            {
                Street = address.Street,
                City = address.City,
                ZipCode = address.ZipCode
            };
        }
    }
}