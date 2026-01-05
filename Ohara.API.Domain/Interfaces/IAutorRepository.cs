using Ohara.API.Domain.Entities;

namespace Ohara.API.Domain.Interfaces
{
    public interface IAutorRepository
    {
       Task<Autor> CadastrarAutor(Autor autor);
    }
}
