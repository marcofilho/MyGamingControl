using Microsoft.Extensions.DependencyInjection;
using MyGameIO.Business.Interfaces;
using MyGameIO.Business.Notificacoes;
using MyGameIO.Business.Services;
using MyGameIO.Data.Context;
using MyGameIO.Data.Repositories;

namespace MyGameIO.Api.Configuration
{
    public static class DependencyInjectConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {

            services.AddScoped<ProjectDbContext>();

            services.AddScoped<IAmigoRepository, AmigoRepository>();
            services.AddScoped<IJogoRepository, JogoRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();

            services.AddScoped<INotificador, Notificador>();

            services.AddScoped<IAmigoService, AmigoService>();
            services.AddScoped<IJogoService, JogoService>();


            return services;
        }
    }
}
