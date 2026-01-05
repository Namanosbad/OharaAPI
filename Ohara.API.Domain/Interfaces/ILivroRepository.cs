using Ohara.API.Domain.Entities;
using Ohara.API.Domain.Enums;

namespace Ohara.API.Domain.Interfaces
{
    public interface ILivroRepository
    {
        Task<IEnumerable<Livro>> LivroPorGenero(EGenero genero);
        Task<IEnumerable<Livro>> BuscarPorTituloAsync(string titulo);
    }
}