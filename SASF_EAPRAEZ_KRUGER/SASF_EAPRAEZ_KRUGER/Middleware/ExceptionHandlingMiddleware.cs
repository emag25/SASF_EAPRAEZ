using SASF_EAPRAEZ_KRUGER.Middleware.Exceptions.BadRequest;
using SASF_EAPRAEZ_KRUGER.Middleware.Exceptions.NotFound;
using SASF_EAPRAEZ_KRUGER.Modelos;
using SASF_EAPRAEZ_KRUGER.Utils;
using System.Net;
using System.Text.Json;

namespace SASF_EAPRAEZ_KRUGER.Middleware
{
    public class ExceptionHandlingMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió una excepción.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {

            context.Response.ContentType = "application/json";

            ErrorResponse errorResponse = new()
            {
                Mensaje = ex.Message,
                Excepcion = ex.GetType().Name.ToString()
            };

            switch (ex)
            {
                case NotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                case BadRequestException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Mensaje = Constantes.MENSAJE_ERROR_500;
                    break;
            }
            /*
            GenericResponse errorResponse = new()
            {

            };*/

            return context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }

    }
}
