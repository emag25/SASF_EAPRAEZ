namespace SASF_EAPRAEZ_KRUGER.Exceptions.InternalServerError
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
