using Microsoft.AspNetCore.Mvc;

namespace GPScript.NET.src.controladores.ayudadores;

public static class RespuestaAPI
{
    public static IActionResult CrearRespuestaExitosa<T>(T data)
    {
        return new OkObjectResult(new
        {
            Exito = true,
            Datos = data
        });
    }

    public static IActionResult CrearRespuestaError(string mensaje)
    {
        return new BadRequestObjectResult(new
        {
            Exito = false,
            Mensaje = mensaje
        });
    }
}
