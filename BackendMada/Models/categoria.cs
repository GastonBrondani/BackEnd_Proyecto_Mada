using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackendMada.Models;

[MySqlCollation("utf8mb4_general_ci")]
public partial class Categoria
{
    [Key]
    public int id_categoria { get; set; }

    [Required(ErrorMessage = "El nombre de la categoría es obligatorio")]
    [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
    public string nombre_categoria { get; set; } = null!;
 
  

    [InverseProperty("id_categoriaNavigation")]
    public virtual ICollection<Producto> productos { get; set; } = new List<Producto>();
}
