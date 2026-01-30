using Ohara.API.Shared.Enums;
using Ohara.API.Shared.Requests;
using Ohara.API.Shared.Responses;

namespace Ohara.API.Application.Interfaces
{
    public interface ILivroService
    {
        Task<LivroResponse> AdicionarLivroAsync(LivroRequest adicionarLivroRequest);
        Task<LivroResponse> BuscarLivroAsync(Guid id);
        Task<IEnumerable<LivroResponse>> BuscarTodosLivrosAsync();
        Task<LivroResponse> AtualizarLivroAsync(Guid id, LivroRequest atualizarLivroRequest);
        Task<bool> DeletarLivroAsync(Guid id);
        Task<IEnumerable<LivroResponse>> BuscarPorTituloAsync(string titulo);
        Task<List<LivroResponse>> LivroPorGeneroAsync(EGenero genero);
    }
}