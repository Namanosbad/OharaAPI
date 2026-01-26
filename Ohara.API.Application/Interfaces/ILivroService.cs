using Ohara.API.Application.Requests;
using Ohara.API.Application.Responses;
using Ohara.API.Domain.Entities;
using Ohara.API.Domain.Enums;

namespace Ohara.API.Application.Interfaces
{
    public interface ILivroService
    {
        Task<LivroResponse> AdicionarLivroAsync(LivroRequest adicionarLivroRequest);
        Task<LivroResponse> BuscarLivroAsync(Guid id);
        Task<IEnumerable<LivroResponse>> BuscarTodosLivrosAsync();
        Task<LivroResponse> AtualizarLivroAsync(LivroRequest livrorequest);
        Task<LivroResponse> DeletarLivroAsync(Guid id);
        Task<List<LivroResponse>> BuscarPorTituloAsync(string titulo);
        Task<List<LivroResponse>> LivroPorGenero(EGenero genero);  
    }
}