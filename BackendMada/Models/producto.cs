using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackendMada.Models;

[Table("producto")]
[Index("id_categoria", Name = "id_categoria")]
[Index("id_proveedor", Name = "id_proveedor")]
[MySqlCollation("utf8mb4_general_ci")]
public partial class producto
{
    [Key]
    public int id_producto { get; set; }

    public int id_categoria { get; set; }

    public int id_proveedor { get; set; }

    [StringLength(50)]
    public string nombre_producto { get; set; } = null!;

    [Precision(10, 2)]
    public decimal precio_venta { get; set; }

    [Precision(10, 2)]
    public decimal precio_costo { get; set; }

    public int stock_producto { get; set; }

    [InverseProperty("id_productoNavigation")]
    public virtual ICollection<datalle_venta> datalle_venta { get; set; } = new List<datalle_venta>();

    [ForeignKey("id_categoria")]
    [InverseProperty("productos")]
    public virtual categoria id_categoriaNavigation { get; set; } = null!;

    [ForeignKey("id_proveedor")]
    [InverseProperty("productos")]
    public virtual proveedor id_proveedorNavigation { get; set; } = null!;
}
