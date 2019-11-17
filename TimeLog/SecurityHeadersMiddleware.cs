using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace TimeLog
{
    // ReSharper disable once InconsistentNaming
    public static class IApplicationBuilderExtensions
    {
        public static void UseSecurityHeaders(
            this IApplicationBuilder app)
        {
            app.UseMiddleware<SecurityHeadersMiddleware>();
        }
    }

    // ReSharper disable once ClassNeverInstantiated.Global
    public class SecurityHeadersMiddleware
    {
        private readonly RequestDelegate _next;

        public SecurityHeadersMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add(
                "Content-Security-Policy-Report-Only", "style-src 'self' " +
                                                       "https://stackpath.bootstrapcdn.com https://fonts.googleapis.com;" +
                                                       "frame-ancestors 'none'; ");
            //context.Response.Headers.Add(
            //    "Content-Security-Policy", "frame-ancestors 'none'");

            context.Response.Headers.Add(
                "Feature-Policy", "camera 'none'");

            context.Response.Headers.Add(
                "X-Content-Type-Options", "nosniff");

            context.Response.Headers.Add(
                "Referrer-Policy", "no-referrer");

            await _next(context);
        }
    }
}