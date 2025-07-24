namespace SASF_EAPRAEZ_KRUGER.Exceptions.BadRequest
{
    public class ModelValidationException : BadRequestException
    {

        public IDictionary<string, string[]>? Errores { get; set; }

        public ModelValidationException(IDictionary<string, string[]> errores)
            : base("Uno o más campos tienen errores.")
        {
            Errores = errores;
        }

    }
}
