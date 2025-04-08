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

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
    public string nombre { get; set; } = null!;

    [Required(ErrorMessage = "El CUIL/CUIT es obligatorio")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "El CUIL/CUIT debe tener exactamente 11 dígitos sin separadores")]
    [StringLength(11)]
    public string cuit_cuil { get; set; } = null!;

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    public string email { get; set; } = null!;

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    [Phone(ErrorMessage = "Formato de teléfono inválido")]
    [StringLength(30)]
    public string telefono { get; set; } = null!;

    [Required(ErrorMessage = "La dirección es obligatoria")]
    [StringLength(100)]
    public string direccion { get; set; } = null!;

    // 💡 ESTA ES LA PARTE CLAVE QUE FALTABA
    [InverseProperty("id_clienteNavigation")]
    public virtual ICollection<venta> venta { get; set; } = new List<venta>();
}
