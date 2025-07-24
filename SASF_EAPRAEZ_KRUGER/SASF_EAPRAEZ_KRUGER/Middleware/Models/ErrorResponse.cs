
namespace SASF_EAPRAEZ_KRUGER.Middleware.Models
{
    public class ErrorResponse
    {

        public string Mensaje { get; set; }

        public string Excepcion { get; set; }

        public IDictionary<string, string[]>? Detalle { get; set; } 

    }
}
