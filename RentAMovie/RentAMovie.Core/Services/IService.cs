using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentAMovie.Core.Services
{
    public interface IService<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(long id);
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(long id);
    }
}