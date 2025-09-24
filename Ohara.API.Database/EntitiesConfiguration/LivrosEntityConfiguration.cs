using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ohara.API.Domain.Entities;

namespace Ohara.API.Database.EntitiesConfiguration
{
    //baixa o entityframeworkcore e herda de IEntityTypeConfig.
    public class LivrosEntityConfiguration : IEntityTypeConfiguration<Livro>
    {
        //implementa a interface
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            //para tabela livros
            builder.ToTable("Livros");
            //primaryKey
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                        .IsRequired()
                        .ValueGeneratedOnAdd();

            builder.Property(x => x.Titulo)
                        .IsRequired()
                        .HasMaxLength(100);
                        
            builder.Property(x => x.Genero)
                        //Já converter em string no banco.
                        .HasConversion<string>()
                        .IsRequired();

            builder.Property(x => x.Descricao)
                        .IsRequired()
                        .HasMaxLength(500);

            builder.Property(x => x.Assunto)
                        .IsRequired()
                        .HasMaxLength(200);

            builder.Property(x => x.ExemplarNumero)
                        .IsRequired();

            builder.Property(x => x.ValorPago)
                        .IsRequired()
                        .HasColumnType("decimal(10,2)");

            builder.Property(x => x.Idioma)
                        .IsRequired()
                        .HasMaxLength(50);

            builder.Property(x => x.DataPublicacao)
                        .IsRequired();

            builder.Property(x => x.ISBN)
                        .HasMaxLength(20);
                        
            builder.Property(x => x.Disponivel)
                        .IsRequired(); 

            builder.HasOne(x => x.Autor)
                        .WithMany(a=> a.Livros)
                        .HasForeignKey(x => x.AutorId);
        }
    }
}
