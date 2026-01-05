using Ohara.API.Domain.Interfaces;

namespace Ohara.API.Domain.Entities
{
    public class Autor : IEntity
    {
        public Guid Id {  get; set; }
        public string Nome { get; set; }
        public List<Livro> Livros { get; set; } = new List<Livro>();
    }
}
