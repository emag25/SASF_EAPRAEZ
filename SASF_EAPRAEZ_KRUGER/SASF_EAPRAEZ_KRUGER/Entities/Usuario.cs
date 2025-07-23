using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SASF_EAPRAEZ_KRUGER.Entities;

[Index("Telefono", Name = "UQ__Usuario__4EC50480D52FFCB2", IsUnique = true)]
[Index("Correo", Name = "UQ__Usuario__60695A19D7618395", IsUnique = true)]
public partial class Usuario
{
    [Key]
    public Guid UsuarioId { get; set; }

    [StringLength(200)]
    public string NombreCompleto { get; set; } = null!;

    [StringLength(100)]
    public string Correo { get; set; } = null!;

    [StringLength(10)]
    public string Telefono { get; set; } = null!;

    [StringLength(20)]
    public string Rol { get; set; } = null!;

    [StringLength(10)]
    public string Estado { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    [InverseProperty("Usuario")]
    public virtual ICollection<Proyecto> Proyecto { get; set; } = new List<Proyecto>();
}
