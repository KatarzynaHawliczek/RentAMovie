using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentAMovie.Infrastructure.Context;
using RentAMovie.Infrastructure.Model;

namespace RentAMovie.Infrastructure.Logic
{
    public class BorrowRepository : IBorrowRepository
    {
        private readonly MovieContext _movieContext;

        public BorrowRepository(MovieContext movieContext)
        {
            _movieContext = movieContext;
        }
        
        public async Task<IEnumerable<Borrow>> GetAll()
        {
            var borrows = await _movieContext.Borrow.ToListAsync();
            borrows.ForEach(x => { _movieContext.Entry(x).Reference(y => y.Client).LoadAsync(); });
            borrows.ForEach(x => { _movieContext.Entry(x).Reference(y => y.Movie).LoadAsync(); });
            return borrows;
        }

        public async Task<Borrow> GetById(long id)
        {
            var borrow = await _movieContext.Borrow
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
            try
            {
                await _movieContext.Entry(borrow).Reference(x => x.Client).LoadAsync();
                await _movieContext.Entry(borrow).Reference(x => x.Movie).LoadAsync();
            }
            catch (ArgumentException e)
            {
                return null;
            }
            return borrow;
        }

        public async Task Add(Borrow borrow)
        {
            borrow.DateOfCreation = DateTime.Now;
            borrow.DateOfBorrow = DateTime.Now;
            borrow.Movie.IsRented = true;
            borrow.Id = null;
            await _movieContext.Borrow.AddAsync(borrow);
            await _movieContext.SaveChangesAsync();
        }

        public async Task Update(Borrow entity)
        {
            var borrowToUpdate = await _movieContext.Borrow
                .Include(x => x.Client)
                .Include(x => x.Movie)
                .SingleOrDefaultAsync(x => x.Id == entity.Id);

            if (borrowToUpdate != null)
            {
                borrowToUpdate.DateOfBorrow = entity.DateOfBorrow;
                borrowToUpdate.DateOfReturn = entity.DateOfReturn;
                borrowToUpdate.Client = entity.Client;
                borrowToUpdate.Movie = entity.Movie;
                borrowToUpdate.DateOfUpdate = DateTime.Now;

                await _movieContext.SaveChangesAsync();
            }
        }

        public async Task Delete(long id)
        {
            var borrowToDelete = await _movieContext.Borrow
                .Include(x => x.Movie)
                .SingleOrDefaultAsync(borrow => borrow.Id == id);
            if (borrowToDelete != null)
            {
                borrowToDelete.Movie.IsRented = false;
                _movieContext.Borrow.Remove(borrowToDelete);
                await _movieContext.SaveChangesAsync();
            }
        }
    }
}