using GPScript.NET.src.aplicaciones.DTOs.jsonDTOs;

namespace GPScript.NET.src.aplicaciones.mapeadores.jsonMapeadores;

public class JsonClienteReductor
{
    public static JsonReducido Reducir(JsonEntrada jsonEntrada, string nombre)
    {
        return new JsonReducido
        {
            NombreEnte = nombre,
            DiasInicioComFinCom = jsonEntrada.FechaEstadoFinal.DayNumber - jsonEntrada.FechaEstadoInicial.DayNumber,
            DiasFinComInicioFactura = jsonEntrada.FechaFacturaInicial.DayNumber - jsonEntrada.FechaEstadoFinal.DayNumber,
            DiasInicioFacturaFinPagado = jsonEntrada.FechaFacturaFinal.DayNumber - jsonEntrada.FechaFacturaInicial.DayNumber,
            DiasInicioComFinPagado = jsonEntrada.FechaFacturaFinal.DayNumber - jsonEntrada.FechaEstadoInicial.DayNumber
        };
    }
}
