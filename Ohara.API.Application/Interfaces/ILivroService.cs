using Ohara.API.Application.Requests;
using Ohara.API.Application.Responses;
using Ohara.API.Domain.Entities;
using Ohara.API.Domain.Enums;

namespace Ohara.API.Application.Interfaces
{
    public interface ILivroService
    {
        Task<AdicionarLivroResponse> AdicionarLivroAsync(AdicionarLivroRequest adicionarLivroRequest);
        Task<Livro> BuscarLivroAsync(Guid id);
        Task<IEnumerable<Livro>> BuscarTodosLivrosAsync();
        Task<Livro> AtualizarLivroAsync(Livro livro);
        Task<Livro> DeletarLivroAsync(Guid id);
        Task<List<Livro>> BuscarPorTituloAsync(string titulo);
        Task<List<Livro>> LivroPorGenero(EGenero genero);  
    }
}