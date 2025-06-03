namespace BackendMada.DTOs.Reporte
{
    public class VentaPorClienteDTO
    {
        public int id_cliente { get; set; }
        public int total_ventas { get; set; }
        public decimal total_monto { get; set; }
    }
}