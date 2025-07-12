using GPScript.NET.src.aplicaciones.DTOs.fastAPI;
using GPScript.NET.src.aplicaciones.DTOs.promedioSujeto;

namespace GPScript.NET.src.infraestructura.repositorios.interfaces;

public interface IFastAPIRepositorio
{
    Task<IEnumerable<ClasificadorRespuesta>> EnviarDatosAsync(IEnumerable<PromedioSujeto> jsonData);
    Task<string> EntrenarModeloAsync(EntrenarModelo jsonData);
}