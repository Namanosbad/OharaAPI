using Ohara.API.Shared.Responses;

namespace Ohara.API.Shared.Requests
{
    public class AutorRequest
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public List<LivroResponse> Livros { get; set; } = new();
    }
}
