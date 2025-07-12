namespace GPScript.NET.src.aplicaciones.DTOs.fastAPI;

public class PrediccionesRespuesta
{
    public string NombreCliente { get; set; } = string.Empty;
    public float Dias_Demora_Real_Promedio { get; set; } = 0;
    public float Dias_Demora_Predicho { get; set; } = 0;
    public float Diferencia_Dias { get; set; } = 0;
}