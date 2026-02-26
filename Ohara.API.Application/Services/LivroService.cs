using AutoMapper;
using Ohara.API.Application.Interfaces;
using Ohara.API.Domain.Entities;
using Ohara.API.Domain.Interfaces;
using Ohara.API.Shared.Enums;
using Ohara.API.Shared.Models;
using Ohara.API.Shared.Requests;
using Ohara.API.Shared.Responses;

namespace Ohara.API.Application.Services
{
    public class LivroService : ILivroService
    {
        private readonly IRepository<Livro> _livroRepo;
        private readonly IRepository<Autor> _autorRepo;
        private readonly ILivroRepository _livroConsultaRepo;
        private readonly IMapper _mapper;

        public LivroService(
            IRepository<Livro> livroRepository,
            IRepository<Autor> autorRepository,
            ILivroRepository livroConsultaRepo,
            IMapper mapper)
        {
            _livroRepo = livroRepository;
            _autorRepo = autorRepository;
            _livroConsultaRepo = livroConsultaRepo;
            _mapper = mapper;
        }

        public async Task<LivroResponse> AdicionarLivroAsync(LivroRequest adicionarLivroRequest)
        {
            if (string.IsNullOrWhiteSpace(adicionarLivroRequest.Titulo))
                throw new ArgumentException("Título é obrigatório.");

            if (string.IsNullOrWhiteSpace(adicionarLivroRequest.NomeAutor))
                throw new ArgumentException("Nome do autor é obrigatório.");

            var autores = await _autorRepo.FindAsync(a => a.Nome == adicionarLivroRequest.NomeAutor);
            var autor = autores.FirstOrDefault();

            if (autor != null)
            {
                var livrosExistentes = await _livroRepo.FindAsync(l =>
                    l.Titulo == adicionarLivroRequest.Titulo &&
                    l.AutorId == autor.Id);

                if (livrosExistentes.Any())
                    throw new BusinessException("Já existe um livro com esse título para esse autor.");
            }
            else
            {
                autor = new Autor
                {
                    Id = Guid.NewGuid(),
                    Nome = adicionarLivroRequest.NomeAutor
                };

                await _autorRepo.AddAsync(autor);
            }

            var livro = _mapper.Map<Livro>(adicionarLivroRequest);
            livro.AutorId = autor.Id;

            var livroSalvo = await _livroRepo.AddAsync(livro);
            var livroResponse = _mapper.Map<LivroResponse>(livroSalvo);
            livroResponse.NomeAutor = autor.Nome;

            return livroResponse;
        }

        public async Task<LivroResponse> AtualizarLivroAsync(Guid id, LivroRequest atualizarLivroRequest)
        {
            var livroExistente = await _livroRepo.GetByIdAsync(id);
            if (livroExistente == null) return null;

            _mapper.Map(atualizarLivroRequest, livroExistente);

            await _livroRepo.UpdateAsync(livroExistente);

            var livroComAutor = await _livroConsultaRepo.ObterPorIdComAutorAsync(id);
            return _mapper.Map<LivroResponse>(livroComAutor ?? livroExistente);
        }

        public async Task<LivroResponse> BuscarLivroAsync(Guid id)
        {
            var livro = await _livroConsultaRepo.ObterPorIdComAutorAsync(id);
            return _mapper.Map<LivroResponse>(livro);
        }

        public async Task<IEnumerable<LivroResponse>> BuscarPorTituloAsync(string titulo)
        {
            var livros = await _livroConsultaRepo.BuscarPorTituloComAutorAsync(titulo);
            return _mapper.Map<IEnumerable<LivroResponse>>(livros);
        }

        public async Task<IEnumerable<LivroResponse>> BuscarTodosLivrosAsync()
        {
            var livros = await _livroConsultaRepo.ObterTodosComAutorAsync();
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
            var livros = await _livroConsultaRepo.BuscarPorGeneroComAutorAsync(genero);
            return _mapper.Map<List<LivroResponse>>(livros);
        }
    }
}
