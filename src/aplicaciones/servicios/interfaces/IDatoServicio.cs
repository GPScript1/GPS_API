using GPScript.NET.src.aplicaciones.DTOs.jsonDTOs;

namespace GPScript.NET.src.aplicaciones.servicios.interfaces;

public interface IDatoServicio
{
    Task<IEnumerable<JsonReducido>> ReducirJson(JsonCompleto[] jsonEntrada);
}
