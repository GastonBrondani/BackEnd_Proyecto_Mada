using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackendMada.Models;

[Table("cliente")]
[MySqlCollation("utf8mb4_general_ci")]
public partial class cliente
{
    [Key]
    public int id_cliente { get; set; }

    [StringLength(40)]
    public string nombre { get; set; } = null!;

    [StringLength(50)]
    public string direccion { get; set; } = null!;

    public int cuit_cuil { get; set; }

    [StringLength(50)]
    public string email { get; set; } = null!;

    [StringLength(30)]
    public string telefono { get; set; } = null!;

    [InverseProperty("id_clienteNavigation")]
    public virtual ICollection<venta> venta { get; set; } = new List<venta>();
}
