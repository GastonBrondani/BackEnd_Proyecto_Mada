using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackendMada.Models;

[Table("proveedor")]
[MySqlCollation("utf8mb4_general_ci")]
public partial class proveedor
{
    [Key]
    public int id_proveedor { get; set; }

    [StringLength(50)]
    public string nombre_proveedor { get; set; } = null!;

    [StringLength(13)]
    public string cuil_proveedor { get; set; } = null!;

    [StringLength(50)]
    public string email_proveedor { get; set; } = null!;

    [StringLength(30)]
    public string telefono_proveedor { get; set; } = null!;

    [InverseProperty("id_proveedorNavigation")]
    public virtual ICollection<producto> productos { get; set; } = new List<producto>();
}
