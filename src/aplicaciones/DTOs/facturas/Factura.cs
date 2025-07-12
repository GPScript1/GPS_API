using GPScript.NET.src.aplicaciones.DTOs.Estados;

namespace GPScript.NET.src.aplicaciones.DTOs.facturas;

public class Factura
{
    public string Numero { get; set; } = string.Empty;
    public string FechaFacturacion { get; set; } = string.Empty;
    public int NumeroEstadosFactura { get; set; }
    public List<EstadoFactura> EstadosFactura { get; set; } = new();
}