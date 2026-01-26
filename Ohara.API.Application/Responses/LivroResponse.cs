using Ohara.API.Domain.Entities;
using Ohara.API.Domain.Enums;

namespace Ohara.API.Application.Responses
{
    public class LivroResponse
    {
        public Guid Id { get; set; }
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
        public Guid AutorId { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}