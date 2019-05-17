using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentAMovie.Infrastructure.Context;
using RentAMovie.Infrastructure.Model;

namespace RentAMovie.Infrastructure.Logic
{
    public class AddressRepository : IAddressRepository
    {
        private readonly MovieContext _movieContext;

        public AddressRepository(MovieContext movieContext)
        {
            _movieContext = movieContext;
        }
        
        public async Task<IEnumerable<Address>> GetAll()
        {
            var addresses = await _movieContext.Address.ToListAsync();
            return addresses;
        }

        public async Task<Address> GetById(long id)
        {
            var address = await _movieContext.Address
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
            return address;
        }

        public async Task Add(Address address)
        {
            address.DateOfCreation = DateTime.Now;
            await _movieContext.Address.AddAsync(address);
            await _movieContext.SaveChangesAsync();
        }

        public async Task Update(Address entity)
        {
            var addressToUpdate = await _movieContext.Address
                .SingleOrDefaultAsync(x => x.Id == entity.Id);

            if (addressToUpdate != null)
            {
                addressToUpdate.Street = entity.Street;
                addressToUpdate.City = entity.City;
                addressToUpdate.ZipCode = entity.ZipCode;
                addressToUpdate.DateOfUpdate = DateTime.Now;
                await _movieContext.SaveChangesAsync();
            }
        }

        public async Task Delete(long id)
        {
            var addressToDelete = await _movieContext.Address.SingleOrDefaultAsync(address => address.Id == id);
            if (addressToDelete != null)
            {
                _movieContext.Address.Remove(addressToDelete);
                await _movieContext.SaveChangesAsync();
            }
        }
    }
}