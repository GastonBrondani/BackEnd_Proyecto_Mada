using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackendMada.Models;

[Index("id_producto", Name = "id_producto")]
[Index("id_venta", Name = "id_venta")]
[MySqlCollation("utf8mb4_general_ci")]
public partial class Detalle_venta
{
    [Key]
    public int id_detalle { get; set; }

    [Required(ErrorMessage = "Debe asociar esta línea de venta a una venta")]
    public int id_venta { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un producto")]
    public int id_producto { get; set; }

    [Required(ErrorMessage = "Debe indicar la cantidad")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a cero")]
    public int cantidad { get; set; }

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a cero")]
    public decimal precio { get; set; }

    [ForeignKey("id_producto")]
    public virtual Producto? producto { get; set; }
    [InverseProperty("datalle_venta")]


    public virtual Producto? id_productoNavigation { get; set; }

    [ForeignKey("id_venta")]
    public virtual Venta? venta { get; set; }
    [InverseProperty("datalle_venta")]
    public virtual Venta? id_ventaNavigation { get; set; }


}
