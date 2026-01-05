using Microsoft.EntityFrameworkCore;
using Ohara.API.Domain.Interfaces;

namespace Ohara.API.Database.Repositories
{
    public class EFRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly OharaDbContext _oharaDbContext;
        private readonly DbSet<T> _DbSet; 
        
        public EFRepository(OharaDbContext oharaDbContext)
        {
            _oharaDbContext = oharaDbContext;
            _DbSet = oharaDbContext.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            _DbSet.Add(entity);
            await _oharaDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _DbSet.FindAsync(id);
            if (entity is null)
                return;

            _DbSet.Remove(entity);
            await _oharaDbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(Guid id)
        {
            var entity = await _DbSet.FindAsync(id);
            if (entity is null) 
                return false;
            else return true;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _DbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _DbSet.FindAsync(id);
        }

        public async Task<T> UpdateAsync(T Tentity)
        {
            _DbSet.Update(Tentity);
            await _oharaDbContext.SaveChangesAsync();
            return Tentity;       
        }
    }
}