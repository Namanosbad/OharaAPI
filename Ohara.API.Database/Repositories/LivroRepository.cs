using Microsoft.EntityFrameworkCore;
using Ohara.API.Domain.Entities;
using Ohara.API.Domain.Interfaces;
using Ohara.API.Shared.Enums;

namespace Ohara.API.Database.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly OharaDbContext _dbContext;

        public LivroRepository(OharaDbContext oharaDbContext)
        {
            _dbContext = oharaDbContext;
        }

        public async Task<Livro?> ObterPorIdComAutorAsync(Guid id)
        {
            return await _dbContext.Livros
                .AsNoTracking()
                .Include(l => l.Autor)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Livro>> ObterTodosComAutorAsync()
        {
            return await _dbContext.Livros
                .AsNoTracking()
                .Include(l => l.Autor)
                .ToListAsync();
        }

        public async Task<IEnumerable<Livro>> BuscarPorTituloComAutorAsync(string titulo)
        {
            return await _dbContext.Livros
                .AsNoTracking()
                .Include(l => l.Autor)
                .Where(l => l.Titulo != null && l.Titulo.Contains(titulo))
                .ToListAsync();
        }

        public async Task<IEnumerable<Livro>> BuscarPorGeneroComAutorAsync(EGenero genero)
        {
            return await _dbContext.Livros
                .AsNoTracking()
                .Include(l => l.Autor)
                .Where(l => l.Genero == genero)
                .ToListAsync();
        }
    }
}
