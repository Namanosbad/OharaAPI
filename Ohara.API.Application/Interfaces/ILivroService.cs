using Ohara.API.Domain.Entities;
using Ohara.API.Domain.Enums;

namespace Ohara.API.Application.Interfaces
{
    public interface ILivroService
    {
        Task<Livro> AdicionarLivroAsync(Livro livro);
        Task<Livro> BuscalLivroAsync(Guid id);
        Task<Livro> BuscarTodosLivrosAsync();
        Task<Livro> AtualizarLivroAsync(Livro livro);
        Task<Livro> DeletarLivroAsync(Guid id);
        Task<List<Livro>> BuscarPorTituloAsync(string titulo);
        Task<List<Livro>> LivroPorGenero(EGenero genero);
        
    }
}
