using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ohara.API.Application.Interfaces;
using Ohara.API.Application.Mappings;
using Ohara.API.Application.Services;
using Ohara.API.Database;
using Ohara.API.Database.Repositories;
using Ohara.API.Domain.Interfaces;
using Ohara.API.Shared.Configuration;
using AutoMapper;

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
            services.Configure<DbConfig>(config => configuration.GetRequiredSection(nameof(DbConfig)).Bind(config));

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
            services.AddScoped<IAutorRepository, AutorRepository>();
            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            services.AddScoped<ILivroService, LivroService>();
            services.AddScoped<IAutorService, AutorService>();
            services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);
            return services;
        }

    }
}