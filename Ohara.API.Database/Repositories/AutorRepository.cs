using Microsoft.EntityFrameworkCore;
using Ohara.API.Domain.Entities;
using Ohara.API.Domain.Interfaces;
using Ohara.API.Shared.Responses;

namespace Ohara.API.Database.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly OharaDbContext _dbContext;
        public AutorRepository(OharaDbContext oharaDbContext)
        {
            _dbContext = oharaDbContext;
        }

        public async Task<Autor> LivroPorAutorAsync(Guid autorId)
        {
            return await _dbContext.Autor
                .Include(a => a.Livros)
                .FirstOrDefaultAsync(a => a.Id == autorId);
        }

        public async Task<IEnumerable<Autor>> ListarAsync()
        {
            return await _dbContext.Autor
              .Include(a => a.Livros)
                .ToListAsync();
        }
        public async Task<Autor> ObterComLivrosPorIdAsync(Guid autorId)
        {
            return await _dbContext.Autor
                .Include(a => a.Livros)
                .FirstOrDefaultAsync(a => a.Id == autorId);
        }
    }
}
