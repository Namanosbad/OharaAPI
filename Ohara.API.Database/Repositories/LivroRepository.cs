using Microsoft.EntityFrameworkCore;
using Ohara.API.Domain.Entities;
using Ohara.API.Domain.Interfaces;

namespace Ohara.API.Database.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly OharaDbContext _dbContext;
        public LivroRepository(OharaDbContext oharaDbContext)
        {
            _dbContext = oharaDbContext;
        }

        public async Task<IEnumerable<Livro>> BuscarPorTituloAsync(string titulo)
        {
            return await _dbContext.Livros
                 .Where(i => i.Titulo.Contains(titulo))
                 .ToListAsync();
        }

        public async Task<IEnumerable<Livro>> LivroPorGenero(string genero)
        {

            return await _dbContext.Livros
                    .AsNoTracking()
                    .Where(i => i.Genero.ToString().Contains(genero))
                    .ToListAsync();
        }
    }
}