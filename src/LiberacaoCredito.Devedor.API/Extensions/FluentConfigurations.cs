using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;


namespace LiberacaoCredito.Devedor.API.Extensions
{
    public static class FluentConfigurations
    {
        public static void AddFluentConfigurations(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    List<string> errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(p => p.ErrorMessage)).ToList();
                    return new BadRequestObjectResult(errors);
                };
            });
        }
    }
}
