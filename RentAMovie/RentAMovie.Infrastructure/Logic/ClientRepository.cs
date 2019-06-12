using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentAMovie.Infrastructure.Context;
using RentAMovie.Infrastructure.Model;

namespace RentAMovie.Infrastructure.Logic
{
    public class ClientRepository : IClientRepository
    {
        private readonly MovieContext _movieContext;

        public ClientRepository(MovieContext movieContext)
        {
            _movieContext = movieContext;
        }
        
        public async Task<IEnumerable<Client>> GetAll()
        {
            var clients = await _movieContext.Client.ToListAsync();
            return clients;
        }

        public async Task<Client> GetById(long id)
        {
            var client = await _movieContext.Client
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
            return client;
        }
    
        public async Task<IEnumerable<Client>> GetByLastName(string lastName)
        {
            var clients = await _movieContext.Client
                .Where(x => x.LastName == lastName)
                .ToListAsync();
            return clients;
        }

        public async Task Add(Client client)
        {
            client.DateOfCreation = DateTime.Now;
            client.Id = null;
            await _movieContext.Client.AddAsync(client);
            await _movieContext.SaveChangesAsync();
        }

        public async Task Update(Client entity)
        {
            var clientToUpdate = await _movieContext.Client
                .Include(x => x.Borrows)
                .SingleOrDefaultAsync(x => x.Id == entity.Id);

            if (clientToUpdate != null)
            {
                clientToUpdate.FirstName = entity.FirstName;
                clientToUpdate.LastName = entity.LastName;
                clientToUpdate.Email = entity.Email;
                clientToUpdate.PhoneNumber = entity.PhoneNumber;
                clientToUpdate.DateOfUpdate = DateTime.Now;

                await _movieContext.SaveChangesAsync();
            }
        }

        public async Task Delete(long id)
        {
            var clientToDelete = await _movieContext.Client.SingleOrDefaultAsync(client => client.Id == id);
            if (clientToDelete != null)
            {
                _movieContext.Client.Remove(clientToDelete);
                await _movieContext.SaveChangesAsync();
            }
        }
    }
}