using System.Threading.Tasks;
using RentAMovie.Contract.Dto;

namespace RentAMovie.Core.Services
{
    public interface IClientService : IService<ClientDto>
    {
        Task<ClientDto> GetByLastName(string lastName);
    }
}