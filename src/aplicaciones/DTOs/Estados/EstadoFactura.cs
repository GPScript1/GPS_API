namespace GPScript.NET.src.aplicaciones.DTOs.Estados;

public class EstadoFactura
{
    public int Estado { get; set; }
    public string Fecha { get; set; } = string.Empty;
    public int Pagado { get; set; }
    public string? Observacion { get; set; }
    public string Usuario { get; set; } = string.Empty;
}