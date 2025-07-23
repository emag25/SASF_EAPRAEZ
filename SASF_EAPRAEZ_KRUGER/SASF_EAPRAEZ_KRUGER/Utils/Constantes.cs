using System;

namespace SASF_EAPRAEZ_KRUGER.Utils
{
    public class Constantes
    {

        public const string ESTADO_ACTIVO = "ACTIVO";
        public const string ESTADO_INACTIVO = "INACTIVO";
        public const string ESTADO_ELIMINADO = "ELIMINADO";

        public const int MAXIMO_HORAS_ESTIMADAS = 1000;
        public const int MAXIMO_HORAS_REALES = 1000;


        // EXPRESIONES REGULARES

        public const string REGEX_NUMERO_LONGITUD_10 = "^\\d{10}$";
        public const string REGEX_ESTADO = $"^({ESTADO_ACTIVO}|{ESTADO_INACTIVO})$";


        // MENSAJES DE ERROR


        public const string MENSAJE_ERROR_500 = "Ha ocurrido un error, intente nuevamente.";

    }
}
