namespace BackendMada.DTOs.Reporte
{
    public class GananciaPorProductoDTO
    {
        public int id_producto { get; set; }
        public string nombre_producto { get; set; } = string.Empty;
        public decimal ganancia_neta { get; set; }
    }
}