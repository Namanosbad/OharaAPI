using AutoMapper;
using Ohara.API.Application.Interfaces;
using Ohara.API.Domain.Entities;
using Ohara.API.Domain.Interfaces;
using Ohara.API.Shared.Models;
using Ohara.API.Shared.Responses;

namespace Ohara.API.Application.Services
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _repository;
        private readonly IRepository<Autor> _repo;
        private readonly IMapper _mapper;
        public AutorService(IAutorRepository autorRepository,IRepository<Autor> repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
            _repository = autorRepository;
        }

        public async Task<IEnumerable<AutorResponse>> ListarAsync()
        {
            var autores = await _repository.ListarAsync();
            return _mapper.Map<IEnumerable<AutorResponse>>(autores);
        }

        public async Task<List<AutorResponse>> AutorAsync(string nome)
        {
            var autores = await _repo.FindAsync(a => a.Nome.Contains(nome));
            return _mapper.Map<List<AutorResponse>>(autores.ToList());
        }

        public async Task<AutorResponse> LivroPorAutorAsync(Guid autorId)
        {
            var autor = await _repo.GetByIdAsync(autorId);
            if (autor == null) throw new BusinessException("nenhum autor encontrado");
            return _mapper.Map<AutorResponse>(autor);
        }
    }
}