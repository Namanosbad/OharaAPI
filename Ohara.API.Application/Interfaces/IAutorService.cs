using Ohara.API.Domain.Entities;

namespace Ohara.API.Application.Interfaces
{
    public interface IAutorService 
    {
        Task<List<Autor>> AutorAsync(string nome);
        Task<Autor> LivroPorAutorAsync(Guid autorId);
    }
}