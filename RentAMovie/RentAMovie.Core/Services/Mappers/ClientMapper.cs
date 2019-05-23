using RentAMovie.Contract.ClientDto;
using RentAMovie.Infrastructure.Model;

namespace RentAMovie.Core.Services.Mappers
{
    internal static class ClientMapper
    {
        public static ClientDto MapClientToDto(Client client)
        {
            return new ClientDto()
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                Street = client.Address.Street,
                City = client.Address.ZipCode,
                ZipCode = client.Address.ZipCode
                //BorrowId = client.Borrow.Id
            };
        }

        public static Client MapDtoToClient(ClientDto client)
        {
            return new Client()
            {
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
                //Borrow = new Borrow()
                //{
                //    Id = client.BorrowId
                //}
            };
        }
    }
}