namespace GPScript.NET.src.aplicaciones.DTOs.jsonDTOs;

public class JsonEntrada
{
    public int IdComercializacion { get; set; }
    public string CodigoCotizacion { get; set; } = string.Empty;
    public string NombreCliente { get; set; } = string.Empty;
    public string LiderComercial { get; set; } = string.Empty;
    public int ValorFinal { get; set; }
    public int EstadoInicial { get; set; }
    public int EstadoFinal { get; set; }
    public DateOnly FechaEstadoInicial { get; set; }
    public DateOnly FechaEstadoFinal { get; set; }
    public DateOnly FechaFacturaInicial { get; set; }
    public DateOnly FechaFacturaFinal { get; set; }
}
