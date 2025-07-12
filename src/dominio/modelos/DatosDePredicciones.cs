namespace GPScript.NET.src.dominio.modelos;

public class DatosDePredicciones
{
    public int Id { get; set; }
    public string NombreEnte { get; set; } = string.Empty;
    public int PromedioInicioComFinCom { get; set; }
    public int PromedioFinComInicioFactura { get; set; }
    public int PromedioInicioFacturaFinPagado { get; set; }
    public int PromedioInicioComFinPagado { get; set; }
    public required string CategoriaRiesgo { get; set; } = string.Empty;
    public float DiasDemoraRealPromedio { get; set; } = 0;
    public float DiasDemoraPredicho { get; set; } = 0;
    public float DiferenciaDias { get; set; } = 0;
}
