using AutoMapper;
using Ohara.API.Application.Interfaces;
using Ohara.API.Application.Requests;
using Ohara.API.Application.Responses;
using Ohara.API.Domain.Entities;
using Ohara.API.Domain.Enums;
using Ohara.API.Domain.Interfaces;

namespace Ohara.API.Application.Services
{
    public class LivroService : ILivroService
    {
        private readonly IRepository<Livro> _livroRepo;
        private readonly IRepository<Autor> _autorRepo;
        private readonly IMapper _mapper;
        public LivroService(IRepository<Livro> livroRepository, IRepository<Autor> autorRepository, IMapper mapper)
        {
            _livroRepo = livroRepository;
            _autorRepo = autorRepository;
            _mapper = mapper;
        }

        public async Task<LivroResponse> AdicionarLivroAsync(LivroRequest adicionarLivroRequest)
        {
            // 1. Busca o autor pelo nome que veio no Request
            var autores = await _autorRepo.FindAsync(a => a.Nome == adicionarLivroRequest.NomeAutor);
            var autor = autores.FirstOrDefault();

            if (autor == null)
            {
                autor = new Autor { Id = Guid.NewGuid(), Nome = adicionarLivroRequest.NomeAutor };
                await _autorRepo.AddAsync(autor);
            }

            // 2. Mapeia o Request para a Entidade Livro
            var livro = _mapper.Map<Livro>(adicionarLivroRequest);

            // 3. Vincula o ID do autor encontrado/criado
            livro.AutorId = autor.Id;

            // 4. Salva e retorna o DTO
            var livroSalvo = await _livroRepo.AddAsync(livro);
            return _mapper.Map<LivroResponse>(livroSalvo);
        }
        public async Task<LivroResponse> AtualizarLivroAsync(Guid id, LivroRequest atualizarLivroRequest)
        {
            var livroExistente = await _livroRepo.GetByIdAsync(id);
            if (livroExistente == null) return null;

            _mapper.Map(atualizarLivroRequest, livroExistente);

            await _livroRepo.UpdateAsync(livroExistente);
            return _mapper.Map<LivroResponse>(livroExistente);
        }

        public async Task<LivroResponse> BuscarLivroAsync(Guid id)
        {
            var livro = await _livroRepo.GetByIdAsync(id);
            return _mapper.Map<LivroResponse>(livro);
        }

        public async Task<IEnumerable<LivroResponse>> BuscarPorTituloAsync(string titulo)
        {
            var livros = await _livroRepo.FindAsync(l => l.Titulo.Contains(titulo));
            return _mapper.Map<IEnumerable<LivroResponse>>(livros);
        }

        public async Task<IEnumerable<LivroResponse>> BuscarTodosLivrosAsync()
        {
            var livros = await _livroRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<LivroResponse>>(livros);
        }

        public async Task<bool> DeletarLivroAsync(Guid id)
        {
            var livro = await _livroRepo.GetByIdAsync(id);
            if (livro == null) return false;
            await _livroRepo.DeleteAsync(id);
            return true;
        }

        public async Task<List<LivroResponse>> LivroPorGeneroAsync(EGenero genero)
        {
            // Busca no repositório filtrando pelo valor do Enum
            var livros = await _livroRepo.FindAsync(l => l.Genero == genero);

            // Converte para a lista de Responses usando o AutoMapper
            return _mapper.Map<List<LivroResponse>>(livros.ToList());
        }
    }
}