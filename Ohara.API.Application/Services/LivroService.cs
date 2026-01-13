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
        private readonly ILivroRepository _livroRepository;
        private readonly IAutorRepository _autorRepository;
        public LivroService(ILivroRepository livroRepository, IAutorRepository autorRepository)
        {
            _livroRepository = livroRepository;
            _autorRepository = autorRepository;
        }

        public async Task<AdicionarLivroResponse> AdicionarLivroAsync(AdicionarLivroRequest adicionarLivroRequest)
        {
            var autor = new Autor
            {
                Id = Guid.NewGuid(),
                Nome = adicionarLivroRequest.NomeAutor
            };

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

            // O que retorna para o usuário
            return await Task.FromResult(new AdicionarLivroResponse
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
                Autor = livro.Autor,
                AutorId = livro.AutorId,
                DataCadastro = livro.DataCadastro
            });

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