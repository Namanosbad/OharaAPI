using Ohara.API.Shared.Enums;

namespace Ohara.API.Shared.Requests
{
    public class LivroRequest
    {
        public string Titulo { get; set; }
        public EGenero Genero { get; set; }
        public string Descricao { get; set; }
        public string Assunto { get; set; }
        public int ExemplarNumero { get; set; }
        public decimal ValorPago { get; set; }
        public string Idioma { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string? ISBN { get; set; }
        public bool Disponivel { get; set; }
        public string NomeAutor { get; set; }
    }
}