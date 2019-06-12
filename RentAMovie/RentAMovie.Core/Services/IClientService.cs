using System.Collections.Generic;
using System.Threading.Tasks;
using RentAMovie.Contract.Dto;

namespace RentAMovie.Core.Services
{
    public interface IClientService : IService<ClientDto>
    {
        Task<IEnumerable<ClientDto>> GetByLastName(string lastName);
    }
}