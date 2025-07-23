using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SASF_EAPRAEZ_KRUGER.Entities;

public partial class Proyecto
{
    [Key]
    public Guid ProyectoId { get; set; }

    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [StringLength(500)]
    public string? Descripcion { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public Guid? UsuarioId { get; set; }

    [StringLength(10)]
    public string Estado { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    [InverseProperty("Proyecto")]
    public virtual ICollection<Actividad> Actividad { get; set; } = new List<Actividad>();

    [ForeignKey("UsuarioId")]
    [InverseProperty("Proyecto")]
    public virtual Usuario? Usuario { get; set; }
}
