using Ohara.API.Application.Interfaces;
using Ohara.API.Domain.Entities;
using Ohara.API.Domain.Interfaces;

namespace Ohara.API.Application.Services
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _autorRepository;
        private readonly IRepository<Autor> _repo;

        public AutorService(IAutorRepository autorRepository, IRepository<Autor> repository) 
        {
            _autorRepository = autorRepository;
            _repo = repository;
        }

        public async Task<List<Autor>> AutorAsync(string nome)
        {
            nome = nome.Trim();
            return await _repo.FindAsync(i => i.Nome.Contains(nome));
        }

        public async Task<Autor> LivroPorAutorAsync(Guid autorId)
        {
            return await _autorRepository.LivroPorAutorAsync(autorId);
        }
    }
}