namespace SASF_EAPRAEZ_KRUGER.Exceptions.BadRequest
{
    public class InvalidIdException : BadRequestException
    {
        public InvalidIdException(string mensaje) : base(mensaje)
        {
        }
    }
}