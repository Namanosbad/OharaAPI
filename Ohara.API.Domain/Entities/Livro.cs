using Ohara.API.Domain.Enums;
using Ohara.API.Domain.Interfaces;

namespace Ohara.API.Domain.Entities;

public class Livro : IEntity
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

    public Guid AutorId { get; set; }
    public Autor Autor { get; set; }
    public DateTime DataCadastro { get; set; } = DateTime.Now;
}