using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentAMovie.Contract.ClientDto;
using RentAMovie.Core.Services.Mappers;
using RentAMovie.Infrastructure.Logic;

namespace RentAMovie.Core.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _iClientRepository;

        public ClientService(IClientRepository iClientRepository)
        {
            _iClientRepository = iClientRepository;
        }

        public async Task<IEnumerable<ClientDto>> GetAll()
        {
            var clients = await _iClientRepository.GetAll();
            return clients
                .Select(ClientMapper.MapClientToDto)
                .ToList();
        }

        public async Task<ClientDto> GetById(long id)
        {
            var client = await _iClientRepository.GetById(id);
            return ClientMapper.MapClientToDto(client);
        }

        public async Task Add(ClientDto client)
        {
            await _iClientRepository.Add(ClientMapper.MapDtoToClient(client));
        }

        public async Task Update(ClientDto entity)
        {
            await _iClientRepository.Update(ClientMapper.MapDtoToClient(entity));
        }

        public async Task Delete(long id)
        {
            await _iClientRepository.Delete(id);
        }
    }
}