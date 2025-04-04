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

    [Required(ErrorMessage = "Debe seleccionar una categoría")]
    public int id_categoria { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un proveedor")]
    public int id_proveedor { get; set; }

    [Required(ErrorMessage = "El nombre del producto es obligatorio")]
    [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
    public string nombre_producto { get; set; } = null!;

    [Required(ErrorMessage = "El precio de venta es obligatorio")]
    [Range(1, double.MaxValue, ErrorMessage = "El precio de venta debe ser mayor a cero")]
    [Precision(10, 2)]
    public decimal precio_venta { get; set; }

    [Required(ErrorMessage = "El precio de costo es obligatorio")]
    [Range(0, double.MaxValue, ErrorMessage = "El precio de costo no puede ser negativo")]
    [Precision(10, 2)]
    public decimal precio_costo { get; set; }

    [Required(ErrorMessage = "El stock es obligatorio")]
    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
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