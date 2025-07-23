using System.Text.Json;

namespace SASF_EAPRAEZ_KRUGER.Middleware.Exceptions.BadRequest
{
    public class ModelValidationException : BadRequestException
    {

        public ModelValidationException(IDictionary<string, string[]> errors)
            : base("Uno o más campos tienen errores." + JsonSerializer.Serialize(errors))
        {
        }

    }
}
