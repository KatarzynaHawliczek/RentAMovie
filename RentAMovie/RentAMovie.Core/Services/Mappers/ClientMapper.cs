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
                PhoneNumber = client.PhoneNumber
            };
        }

        public static Client MapDtoToClient(ClientDto client)
        {
            return new Client()
            {
                
                Id = client.Id.GetValueOrDefault(),
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber
            };
        }
    }
}