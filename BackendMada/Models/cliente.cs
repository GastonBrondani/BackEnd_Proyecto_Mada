using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackendMada.Models;

[Table("cliente")]
[MySqlCollation("utf8mb4_general_ci")]
public partial class Cliente
{
    [Key]
    public int id_cliente { get; set; }
    
    [Required(ErrorMessage = "Debe poner un nombre al cliente")]
    [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
    public string nombre { get; set; } = null!;

    [Required(ErrorMessage = "El cliente debe tener una direccion")]
    [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
    public string direccion { get; set; } = null!;
    
    //tendriamos que buscar una forma de calcular el cuit, que exista
    [Required(ErrorMessage = "El cliente tiene que tener un cuit/cuil")]
    [StringLength(11, ErrorMessage = "verifique el cuit")]
    public string cuit_cuil { get; set; }

    [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
    public string email { get; set; } = null!;

    [Required(ErrorMessage = "Debe ingresar un numero de telefono al cliente")]
    [StringLength(30, ErrorMessage = "Maximo 30 caracteres")]
    public string telefono { get; set; } = null!;

    [InverseProperty("id_clienteNavigation")]
    public virtual ICollection<Venta> venta { get; set; } = new List<Venta>();
}
