using System.Collections.Generic;
using System.Threading.Tasks;
using RentAMovie.Infrastructure.Model;

namespace RentAMovie.Infrastructure.Logic
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<IEnumerable<Client>> GetByLastName(string lastName);
    }
}