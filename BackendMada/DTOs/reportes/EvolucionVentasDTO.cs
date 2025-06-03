namespace BackendMada.DTOs.Reporte
{
    public class EvolucionVentasDTO
    {
        public string periodo { get; set; } = string.Empty; // Ej: "2025-05-15" o "2025-03"
        public decimal total { get; set; }
    }
}