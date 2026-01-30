using Ohara.API.Shared.Responses;

namespace Ohara.API.Application.Interfaces
{
    public interface IAutorService
    {
        Task<List<AutorResponse>> AutorAsync(string nome);
        Task<IEnumerable<AutorResponse>> ListarAsync();
        Task<AutorResponse> LivroPorAutorAsync(Guid autorId);
    }
}