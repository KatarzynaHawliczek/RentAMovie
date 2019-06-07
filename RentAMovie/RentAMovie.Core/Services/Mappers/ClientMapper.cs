using RentAMovie.Contract.Dto;
using RentAMovie.Infrastructure.Model;

namespace RentAMovie.Core.Services.Mappers
{
    internal static class ClientMapper
    {
        public static ClientDto MapClientToDto(Client client)
        {
            return new ClientDto()
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                Street = client.Address.Street,
                City = client.Address.ZipCode,
                ZipCode = client.Address.ZipCode
            };
        }

        public static Client MapDtoToClient(ClientDto client)
        {
            return new Client()
            {
                
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                Address = new Address()
                {
                    Street = client.Street,
                    City = client.City,
                    ZipCode = client.ZipCode
                }
            };
        }
    }
}