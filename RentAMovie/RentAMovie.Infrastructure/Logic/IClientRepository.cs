using System.Threading.Tasks;
using RentAMovie.Infrastructure.Model;

namespace RentAMovie.Infrastructure.Logic
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<Client> GetByLastName(string lastName);
    }
}