using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SASF_EAPRAEZ_KRUGER.Entities;

public partial class Actividad
{
    [Key]
    public Guid ActividadId { get; set; }

    [StringLength(100)]
    public string Titulo { get; set; } = null!;

    [StringLength(500)]
    public string Descripcion { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public int HorasEstimadas { get; set; }

    public int HorasReales { get; set; }

    public Guid ProyectoId { get; set; }

    [StringLength(10)]
    public string Estado { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    [ForeignKey("ProyectoId")]
    [InverseProperty("Actividad")]
    public virtual Proyecto Proyecto { get; set; } = null!;
}
