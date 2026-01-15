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
        public LivroService(IRepository<Livro> livroRepository, IRepository<Autor> autorRepository)
        {
            _livroRepo = livroRepository;
            _autorRepo = autorRepository;
        }

        public async Task<AdicionarLivroResponse> AdicionarLivroAsync(AdicionarLivroRequest adicionarLivroRequest)
        {
            var autor = new Autor
            {
                Id = Guid.NewGuid(),
                Nome = adicionarLivroRequest.NomeAutor
            };

            autor = await _autorRepo.AddAsync(autor);

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
                Autor = autor,
                AutorId = autor.Id
            };
            await _livroRepo.AddAsync(livro);

            // O que retorna para o usuário
            return new AdicionarLivroResponse
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
                AutorId = autor.Id,
                DataCadastro = livro.DataCadastro
            };
        }

        public Task<Livro> AtualizarLivroAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Livro> BuscalLivroAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Livro>> BuscarPorTituloAsync(string titulo)
        {
            throw new NotImplementedException();
        }

        public Task<Livro> BuscarTodosLivrosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Livro> DeletarLivroAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Livro>> LivroPorGenero(EGenero genero)
        {
            throw new NotImplementedException();
        }
    }
}