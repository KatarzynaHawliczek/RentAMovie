using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentAMovie.Contract.Dto;
using RentAMovie.Core.Services.Mappers;
using RentAMovie.Infrastructure.Logic;
using RentAMovie.Infrastructure.Model;

namespace RentAMovie.Core.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly IBorrowRepository _iBorrowRepository;
        private readonly IMovieRepository _iMovieRepository;
        private readonly IClientRepository _iClientRepository;

        public BorrowService(IBorrowRepository iBorrowRepository, IMovieRepository iMovieRepository, IClientRepository iClientRepository)
        {
            _iBorrowRepository = iBorrowRepository;
            _iMovieRepository = iMovieRepository;
            _iClientRepository = iClientRepository;
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
            Borrow entity = BorrowMapper.MapDtoToBorrow(borrow);
            entity.Movie = await _iMovieRepository.GetById(borrow.MovieId.GetValueOrDefault());
            entity.Client = await _iClientRepository.GetById(borrow.ClientId.GetValueOrDefault());
            await _iBorrowRepository.Add(entity);
        }

        public async Task Update(BorrowDto borrow)
        {
            Borrow entity = BorrowMapper.MapDtoToBorrow(borrow);
            entity.Movie = await _iMovieRepository.GetById(borrow.MovieId.GetValueOrDefault());
            entity.Client = await _iClientRepository.GetById(borrow.ClientId.GetValueOrDefault());
            await _iBorrowRepository.Update(entity);
        }

        public async Task Delete(long id)
        {
            await _iBorrowRepository.Delete(id);
        }
    }
}