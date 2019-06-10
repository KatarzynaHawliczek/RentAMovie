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
            //clients.ForEach(x => { _movieContext.Entry(x).Reference(y => y.Address).LoadAsync(); });
            //clients.ForEach(x => { _movieContext.Entry(x).Reference(y => y.Borrows).LoadAsync(); });
            return clients;
        }

        public async Task<Client> GetById(long id)
        {
            var client = await _movieContext.Client
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
            //try
            //{
            //    await _movieContext.Entry(client).Reference(x => x.Address).LoadAsync();
            //    await _movieContext.Entry(client).Reference(x => x.Borrows).LoadAsync();
            //}
            //catch(ArgumentException e)
            //{
            //    return null;
            //}
            return client;
        }

        public async Task<Client> GetByLastName(string lastName)
        {
            var client = await _movieContext.Client
                .Where(x => x.LastName == lastName)
                .SingleOrDefaultAsync();
            
            await _movieContext.Entry(client).Reference(x => x.Borrows).LoadAsync();
            return client;
        }

        public async Task Add(Client client)
        {
            client.DateOfCreation = DateTime.Now;
            //await _movieContext.Client
            //    .Include(x => x.Address)
            //    .Include(x => x.Borrows)
            //    .FirstAsync();
            client.Id = null;
            await _movieContext.Client.AddAsync(client);
            await _movieContext.SaveChangesAsync();
        }

        public async Task Update(Client entity)
        {
            var clientToUpdate = await _movieContext.Client
                .Include(x => x.Address)
                .Include(x => x.Borrows)
                .SingleOrDefaultAsync(x => x.Id == entity.Id);

            if (clientToUpdate != null)
            {
                clientToUpdate.FirstName = entity.FirstName;
                clientToUpdate.LastName = entity.LastName;
                clientToUpdate.Email = entity.Email;
                clientToUpdate.PhoneNumber = entity.PhoneNumber;
                /*clientToUpdate.Address = entity.Address;
                clientToUpdate.Borrows = entity.Borrows;

                if (entity.Address != null && clientToUpdate.Address != null)
                {
                    entity.Address.Id = clientToUpdate.Address.Id;
                    _movieContext.Entry(clientToUpdate.Address).CurrentValues.SetValues(entity.Address);
                }

                if (entity.Borrows != null && clientToUpdate.Borrows != null)
                {
                    var borrowsToUpdate = clientToUpdate.Borrows.ToList();
                    foreach (var borrow in borrowsToUpdate)
                    {
                        foreach (var entityBorrow in entity.Borrows)
                        {
                            if (borrow.Id == entityBorrow.Id)
                            {
                                _movieContext.Entry(borrowsToUpdate).CurrentValues.SetValues(entity.Borrows);
                            }
                        }
                    }
                }*/

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