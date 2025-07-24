namespace SASF_EAPRAEZ_KRUGER.DTOs.Reporte
{
    public class ReporteActividadDTO
    {
        public DateOnly Fecha { get; set; }

        public string Titulo { get; set; } = null!;

        public int HorasReales { get; set; }

    }
}
