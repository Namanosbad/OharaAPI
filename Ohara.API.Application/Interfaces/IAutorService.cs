using Ohara.API.Application.Responses;

namespace Ohara.API.Application.Interfaces
{
    public interface IAutorService
    {
        Task<List<AutorResponse>> AutorAsync(string nome);
        Task<AutorResponse> LivroPorAutorAsync(Guid autorId);
    }
}