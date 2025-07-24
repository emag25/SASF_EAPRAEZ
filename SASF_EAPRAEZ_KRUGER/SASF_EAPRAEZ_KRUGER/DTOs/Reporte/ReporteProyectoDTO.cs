namespace SASF_EAPRAEZ_KRUGER.DTOs.Reporte
{
    public class ReporteProyectoDTO
    {
        public string Nombre { get; set; } = null!;

        public List<ReporteActividadDTO> Actividades { get; set; } = new();

    }
}
