namespace SASF_EAPRAEZ_KRUGER.Middleware.Exceptions.BadRequest
{
    public class ExistingObjectException : BadRequestException
    {
        public ExistingObjectException(): base("El registro ya existe.")
        {
        }

        public ExistingObjectException(string mensaje) : base("El registro ya existe. " + mensaje)
        {
        }
    }
}
