using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackendMada.Models;

[MySqlCollation("utf8mb4_general_ci")]
public partial class categoria
{
    [Key]
    public int id_categoria { get; set; }

    [StringLength(50)]
    public string nombre_categoria { get; set; } = null!;

    [InverseProperty("id_categoriaNavigation")]
    public virtual ICollection<producto> productos { get; set; } = new List<producto>();
}
