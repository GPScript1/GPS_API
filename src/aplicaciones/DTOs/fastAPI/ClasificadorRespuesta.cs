namespace GPScript.NET.src.aplicaciones.DTOs.fastAPI;

public class ClasificadorRespuesta
{
    public string nombreEnte { get; set; } = string.Empty;
    public int promedioInicioComFinCom { get; set; }
    public int promedioFinComInicioFactura { get; set; }
    public int promedioInicioFacturaFinPagado { get; set; }
    public int promedioInicioComFinPagado { get; set; }
    public int cantidadComercializaciones { get; set; }
    public int valorTotalComercializaciones { get; set; }
    public int Cluster { get; set; }
    public string CategoriaRiesgo { get; set; } = string.Empty;
}
