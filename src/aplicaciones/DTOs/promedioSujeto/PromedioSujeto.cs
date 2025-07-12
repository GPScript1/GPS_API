namespace GPScript.NET.src.aplicaciones.DTOs.promedioSujeto;

public class PromedioSujeto
{
    public string NombreEnte { get; set; } = string.Empty;
    public int PromedioInicioComFinCom { get; set; }
    public int PromedioFinComInicioFactura { get; set; }
    public int PromedioInicioFacturaFinPagado { get; set; }
    public int PromedioInicioComFinPagado { get; set; }
    public int CantidadComercializaciones { get; set; }
    public int ValorTotalComercializaciones { get; set; } = 0;
}
