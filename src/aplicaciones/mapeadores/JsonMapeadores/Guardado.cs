using GPScript.NET.src.aplicaciones.DTOs.fastAPI;

namespace GPScript.NET.src.aplicaciones.mapeadores.jsonMapeadores;

public class Guardado
{
    public static GuardadoRespuestas Reducir(string nombre, int promedioInicioComFinCom,
                                             int promedioFinComInicioFactura, int promedioInicioFacturaFinPagado,
                                             int promedioInicioComFinPagado, string categoriaRiesgo,
                                             float diasDemoraRealPromedio,
                                             float diasDemoraPredicho, float diferenciaDias)
    {
        return new GuardadoRespuestas
        {
            NombreEnte = nombre,
            PromedioInicioComFinCom = promedioInicioComFinCom,
            PromedioFinComInicioFactura = promedioFinComInicioFactura,
            PromedioInicioFacturaFinPagado = promedioInicioFacturaFinPagado,
            PromedioInicioComFinPagado = promedioInicioComFinPagado,
            CategoriaRiesgo = categoriaRiesgo,
            DiasDemoraRealPromedio = diasDemoraRealPromedio,
            DiasDemoraPredicho = diasDemoraPredicho,
            DiferenciaDias = diferenciaDias
        };
    }
}