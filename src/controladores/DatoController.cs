using GPScript.NET.src.aplicaciones.DTOs.jsonDTOs;
using GPScript.NET.src.aplicaciones.servicios.interfaces;
using GPScript.NET.src.controladores.ayudadores;
using Microsoft.AspNetCore.Mvc;

namespace GPScript.NET.src.controladores;

[Route("api/[controller]")]
[ApiController]
public class DatoController : ControllerBase
{
    private readonly IDatoServicio _datoServicio;
    public DatoController(IDatoServicio datoServicio)
    {
        _datoServicio = datoServicio;
    }
    [HttpPost("reducir")]
    public async Task<IActionResult> ReducirJson([FromBody] JsonCompleto[] jsonCompleto)
    {
        if (!ModelState.IsValid)
        {
            return RespuestaAPI.CrearRespuestaError("Modelo de entrada no válido.");
        }
        var resultado = await _datoServicio.ReducirJson(jsonCompleto);
        if (resultado == null || !resultado.Any())
        {
            return RespuestaAPI.CrearRespuestaError("No se encontraron datos para reducir.");
        }
        var contador = resultado.Count();
        return RespuestaAPI.CrearRespuestaExitosa(new { contador, resultado });
    }
    [HttpPost("promedio")]
    public async Task<IActionResult> CalcularPromedioSujetos([FromBody] JsonCompleto[] jsonCompleto)
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
        return RespuestaAPI.CrearRespuestaExitosa(promedios);
    }
}
