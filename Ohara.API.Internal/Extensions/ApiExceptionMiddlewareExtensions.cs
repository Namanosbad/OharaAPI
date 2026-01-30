using Microsoft.AspNetCore.Diagnostics;
using Ohara.API.Shared.Models;
using System.Net;

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
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature == null)
                        return;

                    var exception = contextFeature.Error;

                    var statusCode = exception switch
                    {
                        BusinessException => HttpStatusCode.BadRequest,
                        KeyNotFoundException => HttpStatusCode.NotFound,
                        _ => HttpStatusCode.InternalServerError
                    };

                    context.Response.StatusCode = (int)statusCode;

                    var error = new ErrorDetails
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = exception is BusinessException
                            ? exception.Message
                            : "Erro interno no servidor."
                    };

                    await context.Response.WriteAsync(error.ToString());
                });
            });
        }
    }
}