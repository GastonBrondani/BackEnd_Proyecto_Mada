namespace BackendMada.DTOs.Reporte
{
    public class ProductoStockBajoDTO
    {
        public int id_producto { get; set; }
        public string nombre_producto { get; set; } = string.Empty;
        public int stock_actual { get; set; }
    }
}