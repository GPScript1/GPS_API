using GPScript.NET.src.aplicaciones.DTOs.jsonDTOs;

namespace GPScript.NET.src.aplicaciones.mapeadores.JsonMapeadores;

public class JsonCompletoReductor
{
    public static JsonEntrada ReducirJson(JsonCompleto jsonCompleto, int estadoInicial, int estadoFinal,
                                          DateOnly fechaEstadoInicial, DateOnly fechaEstadoFinal,
                                          DateOnly fechaFacturaInicial, DateOnly fechaFacturaFinal)
    {
        return new JsonEntrada
        {
            IdComercializacion = jsonCompleto.IdComercializacion,
            CodigoCotizacion = jsonCompleto.CodigoCotizacion,
            NombreCliente = jsonCompleto.NombreCliente,
            LiderComercial = jsonCompleto.CorreoCreador,
            ValorFinal = jsonCompleto.ValorFinalComercializacion,
            EstadoInicial = estadoInicial,
            EstadoFinal = estadoFinal,
            FechaEstadoInicial = fechaEstadoInicial,
            FechaEstadoFinal = fechaEstadoFinal,
            FechaFacturaInicial = fechaFacturaInicial,
            FechaFacturaFinal = fechaFacturaFinal
        };
    }
}