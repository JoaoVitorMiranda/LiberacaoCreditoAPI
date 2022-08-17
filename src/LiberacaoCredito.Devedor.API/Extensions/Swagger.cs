using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using NSwag;
using NSwag.Generation.Processors.Security;
using System.Linq;

namespace LiberacaoCredito.Devedor.API.Extensions
{
    public static class Swagger
    {
        public static void AddSwaggerConfigurations(this IServiceCollection services, IWebHostEnvironment WebHostEnvironment)
        {
            if (!WebHostEnvironment.IsProduction())
            {
                services.AddSwaggerDocument(document =>
                {
                    document.DocumentName = "v1";
                    document.Version = "v1";
                    document.Title = "API LiberacaoCredito";
                    document.Description = $@"Contém as operações disponíveis para a API LiberacaoCredito";
                });
            }
        }
    }
}
