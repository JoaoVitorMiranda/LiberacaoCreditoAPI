using Microsoft.AspNetCore.Builder;
using LiberacaoCredito.Devedor.API.Middlewares;

namespace LiberacaoCredito.Devedor.API.Extensions
{
    public static class LogExtensions
    {
        public static IApplicationBuilder UseLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogMiddleware>();
        }
    }
}
