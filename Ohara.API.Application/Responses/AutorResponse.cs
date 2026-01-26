using Ohara.API.Domain.Entities;

namespace Ohara.API.Application.Responses
{
    public class AutorResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public List<Livro> Livros { get; set; } = new List<Livro>();
    }
}