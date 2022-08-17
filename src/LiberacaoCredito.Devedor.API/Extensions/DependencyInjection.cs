using LiberacaoCredito.Devedor.API.Services;
using LiberacaoCredito.Devedor.Domain.Interfaces.Notifications;
using LiberacaoCredito.Devedor.Domain.Interfaces.Repository;
using LiberacaoCredito.Devedor.Domain.Interfaces.Services;
using LiberacaoCredito.Devedor.Domain.Interfaces.UoW;
using LiberacaoCredito.Devedor.Domain.Notifications;
using LiberacaoCredito.Devedor.Infra.Context;
using LiberacaoCredito.Devedor.Infra.Repository;
using LiberacaoCredito.Devedor.Infra.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace LiberacaoCredito.Devedor.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {

        public static void ConfigureIoC(
            this IServiceCollection services,
            IConfiguration Configuration)
        {
            AddInfraConfigurations(services, Configuration);
            AddServices(services);
            AddRepositories(services);
        }

        private static void AddInfraConfigurations(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<EntityContext>(options =>
                  options.UseSqlServer(Configuration.GetConnectionString("db")));

            services.AddTransient<IDbConnection>(conn => new SqlConnection(Configuration.GetConnectionString("db")));
            services.AddScoped<DapperContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<ICreditoService, CreditoService>();
            services.AddScoped<IDomainNotification, DomainNotification>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ICreditoRepository, CreditoRepository>();
        }
    }
}
