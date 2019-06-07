using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentAMovie.Contract.Dto;
using RentAMovie.Core.Services.Mappers;
using RentAMovie.Infrastructure.Logic;

namespace RentAMovie.Core.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _iAddressRepository;

        public AddressService(IAddressRepository iAddressRepository)
        {
            _iAddressRepository = iAddressRepository;
        }

        public async Task<IEnumerable<AddressDto>> GetAll()
        {
            var addresses = await _iAddressRepository.GetAll();
            return addresses
                .Select(AddressMapper.MapAddressToDto)
                .ToList();
        }

        public async Task<AddressDto> GetById(long id)
        {
            var address = await _iAddressRepository.GetById(id);
            return AddressMapper.MapAddressToDto(address);
        }

        public async Task Add(AddressDto address)
        {
            await _iAddressRepository.Add(AddressMapper.MapDtoToAddress(address));
        }

        public async Task Update(AddressDto entity)
        {
            await _iAddressRepository.Update(AddressMapper.MapDtoToAddress(entity));
        }

        public async Task Delete(long id)
        {
            await _iAddressRepository.Delete(id);
        }
    }
}