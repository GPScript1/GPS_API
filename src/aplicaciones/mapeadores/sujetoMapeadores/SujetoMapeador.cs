using GPScript.NET.src.aplicaciones.DTOs.jsonDTOs;
using GPScript.NET.src.aplicaciones.DTOs.promedioSujeto;

namespace GPScript.NET.src.aplicaciones.mapeadores.sujetoMapeadores;

public static class SujetoMapeador
{
    public static PromedioSujeto MapearSujeto(JsonReducido jsonReducido,
                                              int promedioInicioComFinCom,
                                              int promedioFinComInicioFactura,
                                              int promedioInicioFacturaFinPagado,
                                              int promedioInicioComFinPagado)
    {
        return new PromedioSujeto
        {
            NombreEnte = jsonReducido.NombreEnte,
            PromedioInicioComFinCom = promedioInicioComFinCom,
            PromedioFinComInicioFactura = promedioFinComInicioFactura,
            PromedioInicioFacturaFinPagado = promedioInicioFacturaFinPagado,
            PromedioInicioComFinPagado = promedioInicioComFinPagado
        };
    }
}