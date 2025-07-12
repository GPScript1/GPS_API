using GPScript.NET.src.aplicaciones.DTOs.fastAPI;

namespace GPScript.NET.src.infraestructura.repositorios.interfaces;

public interface IDatosRepositorio
{
    Task<bool> GuardarDatosAsync(IEnumerable<GuardadoRespuestas> datos);
    Task<IQueryable<GuardadoRespuestas>> ObtenerDatosAsync(string? ente, string? categoria, int? cantidad = 20, int? pagina = 1);
}
