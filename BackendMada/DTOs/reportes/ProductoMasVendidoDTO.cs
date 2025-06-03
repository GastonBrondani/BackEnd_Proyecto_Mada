namespace BackendMada.DTOs.Reporte
{
    public class ProductoMasVendidoDTO
    {
        public int id_producto { get; set; }
        public string nombre_producto { get; set; } = string.Empty;
        public int cantidad_total_vendida { get; set; }
    }
}