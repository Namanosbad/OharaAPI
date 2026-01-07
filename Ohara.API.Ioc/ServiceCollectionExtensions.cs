using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ohara.API.Application.Interfaces;
using Ohara.API.Application.Services;
using Ohara.API.Database;
using Ohara.API.Database.Repositories;
using Ohara.API.Domain.Interfaces;
using Ohara.API.Shared.Configuration;

namespace Ohara.API.Ioc
{

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);
            services.AddApplicationServices(configuration);
            return services;
        }

        // Método de extensão para IServiceCollection, permite configurar o DbContext de forma organizada
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            // Configura a classe DbConfig usando a seção correspondente no appsettings.json
            services.Configure<DbConfig>(config => configuration.GetRequiredSection(nameof(DbConfig)));

            // Adiciona o DbContext ETrocasDbContext ao container de DI
            // O serviço vai usar uma factory que recebe serviceProvider e options do EF
            services.AddDbContext<OharaDbContext>((serviceProvider, options) =>
            {
                // Recupera a configuração do DbConfig injetada via IOptions
                var config = serviceProvider.GetRequiredService<IOptions<DbConfig>>().Value;

                // Pega a connection string da configuração
                var connectionString = config.ConnectionString;

                // Configura o DbContext para usar SQL Server com a connection string obtida
                options.UseSqlServer(connectionString);
            });

            // Retorna a coleção de serviços para permitir encadeamento de chamadas
            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAutorRepository, AutorRepository>();
            services.AddTransient<ILivroRepository, LivroRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            services.AddTransient<IAutorService, AutorService>();
            return services;
        }

    }
}