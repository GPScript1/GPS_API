namespace GPScript.NET.src.aplicaciones.DTOs.jsonDTOs;

public class JsonReducido
{
    public string NombreEnte { get; set; } = string.Empty;
    public int DiasInicioComFinCom { get; set; }
    public int DiasFinComInicioFactura { get; set; }
    public int DiasInicioFacturaFinPagado { get; set; }
    public int DiasInicioComFinPagado { get; set; }
    public int? ValorComercializacion { get; set; }
}
