using Ohara.API.Domain.Entities;
using Ohara.API.Shared.Responses;

namespace Ohara.API.Domain.Interfaces
{
    public interface IAutorRepository
    {
        Task<Autor> LivroPorAutorAsync(Guid autorId);
        Task<IEnumerable<Autor>> ListarAsync(); 
    }
}
