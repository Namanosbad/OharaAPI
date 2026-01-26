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
        private readonly IMapper _maper;
        public LivroService(IRepository<Livro> livroRepository, IRepository<Autor> autorRepository, IMapper mapper)
        {
            _livroRepo = livroRepository;
            _autorRepo = autorRepository;
            _maper = mapper;
        }

        public async Task<LivroResponse> AdicionarLivroAsync(LivroRequest adicionarLivroRequest)
        {
            //verificar se ja tem um livro com esse com esse nome
            var autores = await _autorRepo.FindAsync(a => a.Nome == adicionarLivroRequest.NomeAutor);

            var autor = autores.FirstOrDefault();
            //se nao tiver pode cadastrar o livro
            if (autor == null)
            {
                autor = new Autor
                {
                    Id = Guid.NewGuid(),
                    Nome = adicionarLivroRequest.NomeAutor
                };

                autor = await _autorRepo.AddAsync(autor);
            }
            // Aqui eu crio o Livro (objeto de entrada)
            var livro = new Livro
            {
                Id = Guid.NewGuid(),
                Titulo = adicionarLivroRequest.Titulo,
                Genero = adicionarLivroRequest.Genero,
                Descricao = adicionarLivroRequest.Descricao,
                Assunto = adicionarLivroRequest.Assunto,
                ExemplarNumero = adicionarLivroRequest.ExemplarNumero,
                ValorPago = adicionarLivroRequest.ValorPago,
                Idioma = adicionarLivroRequest.Idioma,
                DataPublicacao = adicionarLivroRequest.DataPublicacao,
                ISBN = adicionarLivroRequest.ISBN,
                Disponivel = adicionarLivroRequest.Disponivel,
                NomeAutor = adicionarLivroRequest.NomeAutor,
                AutorId = autor.Id
            };
            await _livroRepo.AddAsync(livro);

            // O que retorna para o usuário
            return new LivroResponse
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Genero = livro.Genero,
                Descricao = livro.Descricao,
                Assunto = livro.Assunto,
                ExemplarNumero = livro.ExemplarNumero,
                ValorPago = livro.ValorPago,
                Idioma = livro.Idioma,
                DataPublicacao = livro.DataPublicacao, 
                ISBN = livro.ISBN,
                Disponivel = livro.Disponivel,
                NomeAutor = autor.Nome,
                DataCadastro = livro.DataCadastro
            };
        }

        public Task<LivroResponse> AtualizarLivroAsync(LivroRequest livrorequest)
        {
            var atualizarLivro = _livroRepo.UpdateAsync(livrorequest);
            return atualizarLivro;
        }

        public async Task<LivroResponse> BuscarLivroAsync(Guid id)
        {
            var idLivro = await _livroRepo.GetByIdAsync(id);
            return idLivro;
        }

        public async Task<List<LivroResponse>> BuscarPorTituloAsync(string titulo)
        {
            titulo = titulo.Trim();
            return await _livroRepo.FindAsync(i => i.Titulo.Contains(titulo));
        }

        public async Task<IEnumerable<LivroResponse>> BuscarTodosLivrosAsync()
        {
            var busca = await _livroRepo.GetAllAsync();
            return busca.Select(livro => new LivroResponse
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Genero = livro.Genero,
                Descricao = livro.Descricao,
                Assunto = livro.Assunto,
                ExemplarNumero = livro.ExemplarNumero,
                ValorPago = livro.ValorPago,
                Idioma = livro.Idioma,
                DataPublicacao = livro.DataPublicacao,
                ISBN = livro.ISBN,
                Disponivel = livro.Disponivel,
                DataCadastro = livro.DataCadastro
            });
        }

        public async Task<LivroResponse> DeletarLivroAsync(Guid id)
        {
            var deletarLivro = await _livroRepo.DeleteAsync(id);
            return deletarLivro;
        }

        public async Task<List<LivroResponse>> LivroPorGenero(EGenero genero)
        {
            return await _livroRepo.FindAsync(l => l.Genero == genero);
        }
    }
}