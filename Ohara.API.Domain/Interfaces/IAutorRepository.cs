using Ohara.API.Domain.Entities;

namespace Ohara.API.Domain.Interfaces
{
    public interface IAutorRepository
    {
        Task<Autor> LivroPorAutorAsync(Guid autorId);
    }
}
