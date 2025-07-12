using GPScript.NET.src.aplicaciones.DTOs.jsonDTOs;
using GPScript.NET.src.aplicaciones.servicios.interfaces;
using GPScript.NET.src.controladores.ayudadores;
using Microsoft.AspNetCore.Mvc;

namespace GPScript.NET.src.controladores;

[Route("api/[controller]")]
[ApiController]
public class ClasificarController : ControllerBase
{
    private readonly IDatoServicio _datoServicio;

    public ClasificarController(IDatoServicio datoServicio)
    {
        _datoServicio = datoServicio;
    }

    [HttpPost]
    public async Task<IActionResult> ClasificarJson([FromBody] JsonCompleto[] jsonCompleto)
    {
        if (jsonCompleto == null || !jsonCompleto.Any())
        {
            return RespuestaAPI.CrearRespuestaError("No se proporcionaron datos para calcular el promedio.");
        }
        var promedios = await _datoServicio.CalcularPromedioSujetos(jsonCompleto);
        if (promedios == null || !promedios.Any())
        {
            return RespuestaAPI.CrearRespuestaError("No se encontraron promedios calculados.");
        }
        var respuestaFastAPI = await _datoServicio.EnviarDatosAsync(promedios);
        if (respuestaFastAPI == null)
        {
            return RespuestaAPI.CrearRespuestaError("Error al enviar datos a FastAPI.");
        }
        return RespuestaAPI.CrearRespuestaExitosa(respuestaFastAPI);
    }
}