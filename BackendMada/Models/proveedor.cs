using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackendMada.Models;

[Table("proveedor")]
[MySqlCollation("utf8mb4_general_ci")]
public partial class Proveedor
{
    [Key]
    public int id_proveedor { get; set; }

    [Required(ErrorMessage = "El nombre del proveedor es obligatorio")]
    [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
    public string nombre_proveedor { get; set; } = null!;

    [Required(ErrorMessage = "El CUIL es obligatorio")]
    [StringLength(13)]
    public string cuil_proveedor { get; set; } = null!;

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    [StringLength(50)]
    public string email_proveedor { get; set; } = null!;

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    [StringLength(30)]
    public string telefono_proveedor { get; set; } = null!;
 

    [InverseProperty("id_proveedorNavigation")]
    public virtual ICollection<Producto> productos { get; set; } = new List<Producto>();
}
