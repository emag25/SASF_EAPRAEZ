namespace SASF_EAPRAEZ_KRUGER.Middleware.Exceptions.InternalServerError
{
    public class InvalidSettingsException : InternalServerErrorException
    {
        public InvalidSettingsException() : base("La configuración no es válida.")
        {
        }

        public InvalidSettingsException(string mensaje) : base("La configuración no es válida." + mensaje)
        {
        }
    }
}
