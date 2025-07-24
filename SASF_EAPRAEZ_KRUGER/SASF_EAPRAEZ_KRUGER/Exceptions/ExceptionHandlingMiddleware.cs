using SASF_EAPRAEZ_KRUGER.Exceptions.BadRequest;
using SASF_EAPRAEZ_KRUGER.Exceptions.InternalServerError;
using SASF_EAPRAEZ_KRUGER.Exceptions.Models;
using SASF_EAPRAEZ_KRUGER.Exceptions.NotFound;
using SASF_EAPRAEZ_KRUGER.Utils;
using System.Net;
using System.Text.Json;

namespace SASF_EAPRAEZ_KRUGER.Exceptions
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


            ErrorModel errorResponse = new()
            {
                Mensaje = ex.Message,
                Excepcion = ex.GetType().Name.ToString()
            };


            switch (ex)
            {
                case ModelValidationException exception:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Detalle = exception.Errores;
                    break;

                case NotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                case BadRequestException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case InternalServerErrorException:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Mensaje = Constantes.MENSAJE_ERROR_500;
                    break;
            }

            JsonSerializerOptions opcionesJson = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse, opcionesJson));
        }

    }
}
