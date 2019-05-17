using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentAMovie.Infrastructure.Logic
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(long id);
        Task Add(TEntity movie);
        Task Update(TEntity entity);
        Task Delete(long id);
    }
}