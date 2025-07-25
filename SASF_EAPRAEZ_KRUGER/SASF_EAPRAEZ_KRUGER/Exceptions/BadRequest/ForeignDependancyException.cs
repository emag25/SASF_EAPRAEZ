﻿namespace SASF_EAPRAEZ_KRUGER.Exceptions.BadRequest
{
    public class ForeignDependancyException : BadRequestException
    {
        public ForeignDependancyException() : base("El registro tiene dependencias.")
        {
        }

        public ForeignDependancyException(string mensaje) : base("El registro tiene dependencias. " + mensaje)
        {
        }
    }
}
