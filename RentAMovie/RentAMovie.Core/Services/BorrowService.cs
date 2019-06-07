using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentAMovie.Contract.Dto;
using RentAMovie.Core.Services.Mappers;
using RentAMovie.Infrastructure.Logic;

namespace RentAMovie.Core.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly IBorrowRepository _iBorrowRepository;

        public BorrowService(IBorrowRepository iBorrowRepository)
        {
            _iBorrowRepository = iBorrowRepository;
        }

        public async Task<IEnumerable<BorrowDto>> GetAll()
        {
            var borrows = await _iBorrowRepository.GetAll();
            return borrows
                .Select(BorrowMapper.MapBorrowToDto)
                .ToList();
        }

        public async Task<BorrowDto> GetById(long id)
        {
            var borrow = await _iBorrowRepository.GetById(id);
            return BorrowMapper.MapBorrowToDto(borrow);
        }

        public async Task Add(BorrowDto borrow)
        {
            await _iBorrowRepository.Add(BorrowMapper.MapDtoToBorrow(borrow));
        }

        public async Task Update(BorrowDto entity)
        {
            await _iBorrowRepository.Update(BorrowMapper.MapDtoToBorrow(entity));
        }

        public async Task Delete(long id)
        {
            await _iBorrowRepository.Delete(id);
        }
    }
}