using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackendMada.Models;

[Index("id_producto", Name = "id_producto")]
[Index("id_venta", Name = "id_venta")]
[MySqlCollation("utf8mb4_general_ci")]
public partial class datalle_venta
{
    [Key]
    public int id_detalle { get; set; }

    public int id_venta { get; set; }

    public int id_producto { get; set; }

    public int cantidad { get; set; }

    [Precision(10, 2)]
    public decimal precio { get; set; }

    [ForeignKey("id_producto")]
    [InverseProperty("datalle_venta")]
    public virtual producto id_productoNavigation { get; set; } = null!;

    [ForeignKey("id_venta")]
    [InverseProperty("datalle_venta")]
    public virtual venta id_ventaNavigation { get; set; } = null!;
}
