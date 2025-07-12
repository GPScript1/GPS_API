namespace GPScript.NET.src.aplicaciones.DTOs.jsonDTOs;

public class JsonReducido
{
    public string NombreEnte { get; set; } = string.Empty;
    public int DiasInicioComFinCom { get; set; } = 0;
    public int DiasFinComInicioFactura { get; set; } = 0;
    public int DiasInicioFacturaFinPagado { get; set; } = 0;
    public int DiasInicioComFinPagado { get; set; } = 0;
    public int? ValorComercializacion { get; set; } = 0;
}
