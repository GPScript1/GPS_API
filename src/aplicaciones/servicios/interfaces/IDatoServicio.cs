using GPScript.NET.src.aplicaciones.DTOs.fastAPI;
using GPScript.NET.src.aplicaciones.DTOs.jsonDTOs;
using GPScript.NET.src.aplicaciones.DTOs.promedioSujeto;

namespace GPScript.NET.src.aplicaciones.servicios.interfaces;

public interface IDatoServicio
{
    Task<IEnumerable<JsonReducido>> ReducirJson(JsonCompleto[] jsonEntrada);
    Task<IEnumerable<PromedioSujeto>> CalcularPromedioSujetos(JsonCompleto[] jsonCompleto);
    Task<IEnumerable<ClasificadorRespuesta>> EnviarDatosAsync(IEnumerable<PromedioSujeto> jsonData);
    Task<IEnumerable<PrediccionesRespuesta>> EntrenarModeloAsync(JsonCompleto[] jsonCompleto);
    Task<bool> GuardarDatosAsync(IEnumerable<GuardadoRespuestas> datos);
    Task<IQueryable<GuardadoRespuestas>> ObtenerDatosAsync(string? ente, string? categoria, int? cantidad = 20, int? pagina = 1);
}
