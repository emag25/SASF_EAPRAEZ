namespace SASF_EAPRAEZ_KRUGER.Exceptions.Models
{
    public class GenericResponse<T>
    {


        public int Codigo { get; set; }

        public T Contenido { get; set; }


    }
}
