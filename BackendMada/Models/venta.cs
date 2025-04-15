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

    [Required(ErrorMessage = "Debe seleccionar un cliente")]
    public int id_cliente { get; set; }

    [Required(ErrorMessage = "El total de la venta es obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El total debe ser mayor a cero")]
    public decimal total_venta { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime fecha_venta { get; set; }

    [ForeignKey("id_cliente")]
    [InverseProperty("venta")]
    public virtual cliente? id_clienteNavigation { get; set; } 

    [InverseProperty("id_ventaNavigation")]
    public virtual ICollection<datalle_venta> datalle_venta { get; set; } = new List<datalle_venta>();
}
