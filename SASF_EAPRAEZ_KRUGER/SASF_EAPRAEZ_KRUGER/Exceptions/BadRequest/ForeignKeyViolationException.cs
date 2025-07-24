namespace SASF_EAPRAEZ_KRUGER.Exceptions.BadRequest
{
    public class ForeignKeyViolationException : BadRequestException
    {
        public ForeignKeyViolationException() : base("Se intenta hacer referencia a un registro que no existe.")
        {
        }

        public ForeignKeyViolationException(string mensaje) : base("Se intenta hacer referencia a un registro que no existe. " + mensaje)
        {
        }
    }
}
