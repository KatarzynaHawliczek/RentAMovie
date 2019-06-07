using RentAMovie.Contract.Dto;
using RentAMovie.Infrastructure.Model;

namespace RentAMovie.Core.Services.Mappers
{
    internal static class AddressMapper
    {
        public static AddressDto MapAddressToDto(Address address)
        {
            return new AddressDto()
            {
                Id = address.Id,
                Street = address.Street,
                City = address.City,
                ZipCode = address.ZipCode
            };
        }

        public static Address MapDtoToAddress(AddressDto address)
        {
            return new Address()
            {
                Id = address.Id,
                Street = address.Street,
                City = address.City,
                ZipCode = address.ZipCode
            };
        }
    }
}