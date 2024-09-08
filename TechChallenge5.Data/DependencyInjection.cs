using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using TechChallenge5.Data.DataContext;
using TechChallenge5.Data.Repositories;
using TechChallenge5.Domain.Entities;
using TechChallenge5.Domain.Interfaces.Repositories;
using TechChallenge5.Domain.Interfaces.Services;
using TechChallenge5.Domain.Services;

namespace TechChallenge5.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options => 
                options.UseSqlite("Data source=app.db"));

            services.AddAutoMapper(typeof(UsuarioEntity).Assembly);
            services.AddAutoMapper(typeof(PortifolioEntity).Assembly);

            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAtivoService, AtivoService>();
            services.AddScoped<ITransacaoService, TransacaoService>();
            services.AddScoped<IPortifolioService, PortifolioService>();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IAtivoRepository, AtivoRepository>();
            services.AddScoped<ITransacaoRepository, TransacaoRepository>();
            services.AddScoped<IPortifolioRepository, PortifolioRepository>();

            return services;
        }
    }
}
