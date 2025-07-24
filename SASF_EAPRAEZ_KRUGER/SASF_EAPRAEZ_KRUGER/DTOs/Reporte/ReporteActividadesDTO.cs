namespace SASF_EAPRAEZ_KRUGER.DTOs.Reporte
{
    public class ReporteActividadesDTO
    {

        public string Usuario { get; set; } = null!;

        public List<ReporteProyectoDTO> Proyectos { get; set; } = new();

    }
}
