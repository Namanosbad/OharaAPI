using Ohara.API.Domain.Entities;

namespace Ohara.API.Application.Interfaces
{
    public interface IAutorService 
    {
       Task<Autor> LivroPorAutorAsync(Guid autorId);
    }
}