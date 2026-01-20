using System.Linq.Expressions;

namespace Ohara.API.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> UpdateAsync(T Tentity);
        Task<bool> ExistAsync(Guid id);
        Task<T> DeleteAsync(Guid id);
        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
    }
}