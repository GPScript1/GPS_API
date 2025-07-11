using GPScript.NET.src.aplicaciones.DTOs.promedioSujeto;

namespace GPScript.NET.src.infraestructura.repositorios.interfaces;

public interface IFastAPIRepositorio
{
    Task<IEnumerable<PromedioSujeto>> EnviarDatosAsync(IEnumerable<PromedioSujeto> jsonData);
}