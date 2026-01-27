using AutoMapper;
using Ohara.API.Application.Interfaces;
using Ohara.API.Application.Responses;
using Ohara.API.Domain.Entities;
using Ohara.API.Domain.Interfaces;

namespace Ohara.API.Application.Services
{
    public class AutorService : IAutorService
    {
        private readonly IRepository<Autor> _repo;
        private readonly IMapper _mapper;
        public AutorService(IRepository<Autor> repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }

        public async Task<List<AutorResponse>> AutorAsync(string nome)
        {
            var autores = await _repo.FindAsync(a => a.Nome.Contains(nome));
            return _mapper.Map<List<AutorResponse>>(autores.ToList());
        }

        public async Task<AutorResponse> LivroPorAutorAsync(Guid autorId)
        {
            var autor = await _repo.GetByIdAsync(autorId);
            return _mapper.Map<AutorResponse>(autor);
        }
    }
}