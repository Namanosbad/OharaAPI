using Ohara.API.Domain.Entities;
using Ohara.API.Shared.Enums;

namespace Ohara.API.Domain.Interfaces
{
    public interface ILivroRepository
    {
        Task<Livro?> ObterPorIdComAutorAsync(Guid id);
        Task<IEnumerable<Livro>> ObterTodosComAutorAsync();
        Task<IEnumerable<Livro>> BuscarPorTituloComAutorAsync(string titulo);
        Task<IEnumerable<Livro>> BuscarPorGeneroComAutorAsync(EGenero genero);
    }
}
