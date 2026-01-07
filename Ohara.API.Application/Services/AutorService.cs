using Ohara.API.Application.Interfaces;
using Ohara.API.Domain.Entities;
using Ohara.API.Domain.Interfaces;

namespace Ohara.API.Application.Services
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _autorRepository;
        public AutorService(IAutorRepository autorRepository) 
        {
            _autorRepository = autorRepository;
        }

        public async Task<Autor> LivroPorAutorAsync(Guid autorId)
        {
            return await _autorRepository.LivroPorAutorAsync(autorId);
        }
    }
}