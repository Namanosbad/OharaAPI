using Microsoft.EntityFrameworkCore;
using Ohara.API.Database.EntitiesConfiguration;
using Ohara.API.Domain.Entities;

namespace Ohara.API.Database
{
    public class OharaDbContext : DbContext
    {
        //setar banco.
        public DbSet<Autor> Autor { get; set; }
        public DbSet<Livro> Livros { get; set; }

        //gera o construtor pra poder usar a configuração do appsettings.
        public OharaDbContext(DbContextOptions<OharaDbContext> options) : base(options) { }

        //construtor para usar  o modelo do banco fora dessa classe.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AutorEntityConfiguration());
            modelBuilder.ApplyConfiguration(new LivrosEntityConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}