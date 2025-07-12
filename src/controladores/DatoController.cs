using GPScript.NET.src.aplicaciones.DTOs.fastAPI;
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
    [HttpPost("subir")]
    public async Task<IActionResult> SubirDatos([FromBody] JsonCompleto[] jsonCompleto)
    {
        if (jsonCompleto == null || !jsonCompleto.Any())
        {
            return RespuestaAPI.CrearRespuestaError("No se proporcionaron datos para subir.");
        }
        var promedios = await _datoServicio.CalcularPromedioSujetos(jsonCompleto);
        if (promedios == null || !promedios.Any())
        {
            return RespuestaAPI.CrearRespuestaError("Error al subir los datos.");
        }
        var categorias = await _datoServicio.EnviarDatosAsync(promedios);
        if (categorias == null || !categorias.Any())
        {
            return RespuestaAPI.CrearRespuestaError("Error al subir los datos.");
        }
        var predicciones = await _datoServicio.EntrenarModeloAsync(jsonCompleto);
        if (predicciones == null || !predicciones.Any())
        {
            return RespuestaAPI.CrearRespuestaError("Error al entrenar el modelo.");
        }
        var datosAGuardar = categorias.Select(p => new GuardadoRespuestas
        {
            NombreEnte = p.nombreEnte,
            PromedioInicioComFinCom = p.promedioInicioComFinCom,
            PromedioFinComInicioFactura = p.promedioFinComInicioFactura,
            PromedioInicioFacturaFinPagado = p.promedioInicioFacturaFinPagado,
            PromedioInicioComFinPagado = p.promedioInicioComFinPagado,
            CategoriaRiesgo = p.CategoriaRiesgo,

        }).ToList();
        foreach (var ente in datosAGuardar)
        {
            var prediccion = predicciones.FirstOrDefault(p => p.NombreCliente.Equals(ente.NombreEnte));
            ente.DiasDemoraRealPromedio = prediccion?.Dias_Demora_Real_Promedio ?? 0;
            ente.DiasDemoraPredicho = prediccion?.Dias_Demora_Predicho ?? 0;
            ente.DiferenciaDias = prediccion?.Diferencia_Dias ?? 0;
        }
        var resultado = await _datoServicio.GuardarDatosAsync(datosAGuardar);
        return resultado
            ? RespuestaAPI.CrearRespuestaExitosa("Datos guardados exitosamente.")
            : RespuestaAPI.CrearRespuestaError("Error al guardar los datos.");
    }
    [HttpGet("obtener")]
    public async Task<IActionResult> ObtenerDatos([FromQuery] string? ente, [FromQuery] string? categoria, [FromQuery] int? cantidad = 20, [FromQuery] int? pagina = 1)
    {
        var datos = await _datoServicio.ObtenerDatosAsync(ente, categoria, cantidad, pagina);
        if (datos == null || !datos.Any())
        {
            return RespuestaAPI.CrearRespuestaError("No se encontraron datos.");
        }
        return RespuestaAPI.CrearRespuestaExitosa(datos);
    }
}
