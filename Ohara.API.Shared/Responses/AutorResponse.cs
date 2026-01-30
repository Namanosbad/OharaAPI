namespace Ohara.API.Shared.Responses
{
    public class AutorResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public List<LivroResponse> Livros { get; set; } = new ();
    }
}