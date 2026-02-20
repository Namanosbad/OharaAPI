using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Ohara.API.Shared.Models;
using System.Net;
using System.Text.Json;

namespace Ohara.API.Internal.Extensions
{
    public static class ApiExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var logger = context.RequestServices
                        .GetRequiredService<ILoggerFactory>()
                        .CreateLogger("GlobalExceptionHandler");

                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature == null)
                        return;

                    var exception = contextFeature.Error;
                    logger.LogError(exception,
                        "Erro não tratado. Path: {Path} | Método: {Method}",
                        context.Request.Path,
                        context.Request.Method);

                    var statusCode = exception switch
                    {
                        BusinessException => HttpStatusCode.BadRequest,
                        KeyNotFoundException => HttpStatusCode.NotFound,
                        UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                        _ => HttpStatusCode.InternalServerError
                    };

                    context.Response.StatusCode = (int)statusCode;

                    var error = new ErrorDetails
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = exception switch
                        {
                            BusinessException => exception.Message,
                            KeyNotFoundException => exception.Message,
                            UnauthorizedAccessException => "Acesso não autorizado.",
                            _ => "Erro interno no servidor."
                        }
                    };

                    var json = JsonSerializer.Serialize(error);

                    await context.Response.WriteAsJsonAsync(error);
                });
            });
        }
    }
}
