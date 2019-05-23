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
            clients.ForEach(x => { _movieContext.Entry(x).Reference(y => y.Address).LoadAsync(); });
            clients.ForEach(x => { _movieContext.Entry(x).Reference(y => y.Borrow).LoadAsync(); });
            clients.ForEach(x => { _movieContext.Entry(x).Reference(y => y.Movies).LoadAsync(); });
            return clients;
        }

        public async Task<Client> GetById(long id)
        {
            var client = await _movieContext.Client
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
            await _movieContext.Entry(client).Reference(x => x.Address).LoadAsync();
            await _movieContext.Entry(client).Reference(x => x.Borrow).LoadAsync();
            await _movieContext.Entry(client).Reference(x => x.Movies).LoadAsync();
            return client;
        }

        public async Task Add(Client client)
        {
            client.DateOfCreation = DateTime.Now;
            await _movieContext.Client
                .Include(x => x.Address)
                .Include(x => x.Borrow)
                .Include(x => x.Movies)
                .FirstAsync();
            await _movieContext.Client.AddAsync(client);
            await _movieContext.SaveChangesAsync();
        }

        public async Task Update(Client entity)
        {
            var clientToUpdate = await _movieContext.Client
                .Include(x => x.Address)
                .Include(x => x.Borrow)
                .Include(x => x.Movies)
                .SingleOrDefaultAsync(x => x.Id == entity.Id);

            if (clientToUpdate != null)
            {
                clientToUpdate.FirstName = entity.FirstName;
                clientToUpdate.LastName = entity.LastName;
                clientToUpdate.Email = entity.Email;
                clientToUpdate.PhoneNumber = entity.PhoneNumber;
                clientToUpdate.Address = entity.Address;
                clientToUpdate.Borrow = entity.Borrow;
                clientToUpdate.Movies = entity.Movies;

                if (entity.Address != null && clientToUpdate.Address != null)
                {
                    entity.Address.Id = clientToUpdate.Address.Id;
                    _movieContext.Entry(clientToUpdate.Address).CurrentValues.SetValues(entity.Address);
                }

                if (entity.Borrow != null && clientToUpdate.Borrow != null)
                {
                    entity.Borrow.Id = clientToUpdate.Borrow.Id;
                    _movieContext.Entry(clientToUpdate.Borrow).CurrentValues.SetValues(entity.Borrow);
                }

                if (entity.Movies != null && clientToUpdate.Movies != null)
                {
                    var moviesToUpdate = clientToUpdate.Movies.ToList();
                    foreach (var movie in moviesToUpdate)
                    {
                        foreach (var entityMovie in entity.Movies)
                        {
                            if (movie.Id == entityMovie.Id)
                            {
                                _movieContext.Entry(moviesToUpdate).CurrentValues.SetValues(entity.Movies);
                            }
                        }
                    }
                }

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