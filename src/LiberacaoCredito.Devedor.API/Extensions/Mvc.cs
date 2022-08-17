using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using LiberacaoCredito.Devedor.API.Filters;

namespace LiberacaoCredito.Devedor.API.Extensions
{
    public static class Mvc
    {
        public static void AddMvcConfigurations(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add<DomainNotificationFilter>();
                options.EnableEndpointRouting = false;
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            }
            ).AddFluentValidation();
        }
    }
}
