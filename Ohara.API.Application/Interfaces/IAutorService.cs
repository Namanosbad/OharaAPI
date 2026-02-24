using Ohara.API.Shared.Responses;

namespace Ohara.API.Application.Interfaces
{
    public interface IAutorService
    {
        Task<IEnumerable<AutorResponse>> ListarAsync();
        Task<List<AutorResponse>> AutorAsync(string nome);
        Task<AutorResponse> LivroPorAutorAsync(Guid autorId);
        Task DeletarSemLivrosAsync(Guid autorId);
    }
}