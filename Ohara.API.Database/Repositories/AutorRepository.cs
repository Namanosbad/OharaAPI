using Ohara.API.Domain.Entities;
using Ohara.API.Domain.Interfaces;

namespace Ohara.API.Database.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly OharaDbContext _dbContext;
        public AutorRepository(OharaDbContext oharaDbContext)
        {
             _dbContext = oharaDbContext;
        }

        public async Task<Autor> CadastrarAutor(Autor autor)
        {
            await _dbContext.AddAsync(autor);
            await _dbContext.SaveChangesAsync();
            return autor;
        }
    }
}
