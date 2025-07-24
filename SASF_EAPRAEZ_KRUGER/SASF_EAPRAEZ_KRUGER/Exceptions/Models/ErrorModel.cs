using System.Net;

namespace SASF_EAPRAEZ_KRUGER.Exceptions.Models
{
    public class ErrorModel
    {

        public string Mensaje { get; set; }

        public string Excepcion { get; set; }

        public IDictionary<string, string[]>? Detalle { get; set; } 

    }
}
