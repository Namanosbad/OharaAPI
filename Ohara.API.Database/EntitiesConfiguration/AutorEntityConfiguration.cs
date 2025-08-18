using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ohara.API.Domain.Entities;

namespace Ohara.API.Database.EntitiesConfiguration
{
    public class AutorEntityConfiguration : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.ToTable("Autor");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

            builder.Property(a => a.Nome)
                    .IsRequired()
                    .HasMaxLength(100);
        }
    }
}
