using GPScript.NET.src.aplicaciones.servicios.interfaces;
using GPScript.NET.src.controladores.ayudadores;
using GPScript.NET.src.aplicaciones.DTOs.jsonDTOs;
using Microsoft.AspNetCore.Mvc;

namespace GPScript.NET.src.controladores;

[Route("api/[controller]")]
[ApiController]
public class PrediccionController : ControllerBase
{
    private readonly IDatoServicio _datoServicio;

    public PrediccionController(IDatoServicio datoServicio)
    {
        _datoServicio = datoServicio;
    }

    [HttpPost("entrenar")]
    public async Task<IActionResult> EntrenarModelo([FromBody] JsonCompleto[] jsonCompleto)
    {
        if (jsonCompleto == null || !jsonCompleto.Any())
        {
            return RespuestaAPI.CrearRespuestaError("No se proporcionaron datos para entrenar el modelo.");
        }
        var resultado = await _datoServicio.EntrenarModeloAsync(jsonCompleto);
        if (resultado == null || !resultado.Any())
        {
            return RespuestaAPI.CrearRespuestaError("Error al entrenar el modelo.");
        }
        return RespuestaAPI.CrearRespuestaExitosa(resultado);
    }
}
