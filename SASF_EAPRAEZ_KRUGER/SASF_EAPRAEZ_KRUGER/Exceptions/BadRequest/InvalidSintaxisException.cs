namespace SASF_EAPRAEZ_KRUGER.Exceptions.BadRequest
{
    public class InvalidSintaxisException : BadRequestException
    {
        public InvalidSintaxisException() : base("Sintaxis no válida.")
        {
        }
    }
}
