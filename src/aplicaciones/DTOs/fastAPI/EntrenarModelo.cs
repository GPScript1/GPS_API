using GPScript.NET.src.aplicaciones.DTOs.jsonDTOs;
using GPScript.NET.src.aplicaciones.DTOs.promedioSujeto;

namespace GPScript.NET.src.aplicaciones.DTOs.fastAPI;

public class EntrenarModelo
{
    public List<PromedioSujeto> ClientePromedio { get; set; } = new List<PromedioSujeto>();
    public List<JsonReducido> Comercializaciones { get; set; } = new List<JsonReducido>();
}