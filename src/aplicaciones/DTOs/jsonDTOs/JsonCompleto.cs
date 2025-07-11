using GPScript.NET.src.aplicaciones.DTOs.Estados;
using GPScript.NET.src.aplicaciones.DTOs.facturas;

namespace GPScript.NET.src.aplicaciones.DTOs.jsonDTOs;

public class JsonCompleto
{
    public int IdComercializacion { get; set; }
    public string CodigoCotizacion { get; set; } = string.Empty;
    public string FechaInicio { get; set; } = string.Empty;
    public int ClienteId { get; set; }
    public string NombreCliente { get; set; } = string.Empty;
    public string CorreoCreador { get; set; } = string.Empty;
    public int ValorFinalComercializacion { get; set; }
    public int ValorFinalCotizacion { get; set; }
    public int NumeroEstados { get; set; }
    public List<EstadoCom> Estados { get; set; } = new();
    public List<Factura> Facturas { get; set; } = new();
}
