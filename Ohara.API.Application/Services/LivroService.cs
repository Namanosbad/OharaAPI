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
            var autores = await _autorRepo.FindAsync(a => a.Nome == adicionarLivroRequest.NomeAutor);

            var autor = autores.FirstOrDefault();

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

        public Task<Livro> AtualizarLivroAsync(Livro livro)
        {
            var atualizarLivro = _livroRepo.UpdateAsync(livro);
            return atualizarLivro;
        }

        public async Task<Livro> BuscarLivroAsync(Guid id)
        {
            var idLivro = await _livroRepo.GetByIdAsync(id);
            return idLivro;
        }

        public async Task<List<Livro>> BuscarPorTituloAsync(string titulo)
        {
            titulo = titulo.Trim();
            return await _livroRepo.FindAsync(i => i.Titulo.Contains(titulo));
        }

        public Task<IEnumerable<Livro>> BuscarTodosLivrosAsync()
        {
           var busca = _livroRepo.GetAllAsync();
            return busca;
        }

        public async Task<Livro> DeletarLivroAsync(Guid id)
        {
            var deletarLivro = await _livroRepo.DeleteAsync(id);
            return deletarLivro;
        }

        public async Task<List<Livro>> LivroPorGenero(EGenero genero)
        {
            return await _livroRepo.FindAsync(l => l.Genero == genero);
        }
    }
}