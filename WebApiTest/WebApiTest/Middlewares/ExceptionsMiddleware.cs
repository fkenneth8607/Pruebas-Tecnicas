using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApiTest.Middlewares
{
    /// <summary>
    /// Middleware para el manejo de errores
    /// </summary>
    public class ExceptionsMiddleware
    {
        private const string ContentType = "application/json";
        private readonly ILogger<ExceptionsMiddleware> _logger;
        private readonly RequestDelegate _next;

        /// <summary>
        /// Crea una Nueva Instancia de <see cref="ExceptionsMiddleware"/> class.
        /// </summary>
        /// <param name="next"></param>
        public ExceptionsMiddleware(ILogger<ExceptionsMiddleware> logger, RequestDelegate next)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _next = next;
        }

        /// <summary>
        /// Serializa Las Excepciones y Deja Registro en el Logger
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext httpContext) => InvokeAsync(httpContext);

        async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                _logger.LogInformation($"Se está invocando {httpContext.Request.Path.Value} Metodo: {httpContext.Request.Method} - {DateTime.Now}");
                await _next(httpContext);
                _logger.LogInformation($"Se ha invocando {httpContext.Request.Path.Value} Metodo: {httpContext.Request.Method} - {DateTime.Now}");
            }
            catch (Exception ex)
            {
                var statusCode = ConfigurateExceptionTypes(ex);

                httpContext.Response.StatusCode = statusCode;
                httpContext.Response.ContentType = ContentType;

                var error = JsonConvert.SerializeObject(new
                {
                    message = ex.Message,
                    innerexception = ex.InnerException?.StackTrace,
                });

                await httpContext.Response.WriteAsync(error);

                _logger.LogError($"Error: Code {statusCode}. Mensaje: {ex.Message}. InnerException: {ex.InnerException}");

            }
        }

        private static int ConfigurateExceptionTypes(Exception ex)
        {
            int statusCode;

            switch (ex)
            {
                case var _ when ex is ArgumentNullException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case var _ when ex is NotImplementedException:
                    statusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            return statusCode;
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionsMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionsMiddleware>();
        }
    }
}
