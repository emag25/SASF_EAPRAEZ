using SASF_EAPRAEZ_KRUGER.DTOs.Reporte;
using SASF_EAPRAEZ_KRUGER.Entities;

namespace SASF_EAPRAEZ_KRUGER.Mappers
{
    public class ReporteMapper
    {

        public static ReporteActividadesDTO MapearReporte(string nombreUsuario, List<Proyecto> proyectos)
        {
            return new ReporteActividadesDTO
            {
                Usuario = nombreUsuario,
                Proyectos = proyectos.Select(p => new ReporteProyectoDTO
                {
                    Nombre = p.Nombre,
                    Actividades = p.Actividad.Select(a => new ReporteActividadDTO
                    {
                        Fecha = a.Fecha,
                        Titulo = a.Titulo,
                        HorasReales = a.HorasReales
                    }).ToList()

                }).ToList()
            };
        }

    }
}
