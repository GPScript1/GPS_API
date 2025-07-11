using GPScript.NET.src.aplicaciones.DTOs.jsonDTOs;
using GPScript.NET.src.aplicaciones.DTOs.promedioSujeto;

namespace GPScript.NET.src.aplicaciones.servicios.interfaces;

public interface IDatoServicio
{
    Task<IEnumerable<JsonReducido>> ReducirJson(JsonCompleto[] jsonEntrada);
    Task<IEnumerable<PromedioSujeto>> CalcularPromedioSujetos(IEnumerable<JsonReducido> jsonReducido);
}
