using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackendMada.Models;

[Index("id_cliente", Name = "id_cliente")]
[MySqlCollation("utf8mb4_general_ci")]
public partial class venta
{
    [Key]
    public int id_venta { get; set; }

    public int id_cliente { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime fecha_venta { get; set; }

    [Precision(10, 2)]
    public decimal total_venta { get; set; }

    [InverseProperty("id_ventaNavigation")]
    public virtual ICollection<datalle_venta> datalle_venta { get; set; } = new List<datalle_venta>();

    [ForeignKey("id_cliente")]
    [InverseProperty("venta")]
    public virtual cliente id_clienteNavigation { get; set; } = null!;
}
