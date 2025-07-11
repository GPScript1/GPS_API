using System.Globalization;
using System.Linq;
using GPScript.NET.src.aplicaciones.DTOs.jsonDTOs;
using GPScript.NET.src.aplicaciones.mapeadores.JsonMapeadores;
using GPScript.NET.src.aplicaciones.servicios.interfaces;

namespace GPScript.NET.src.aplicaciones.servicios.implementaciones;

public class DatoServicio : IDatoServicio
{
    public async Task<IEnumerable<JsonReducido>> ReducirJson(JsonCompleto[] jsonCompleto)
    {
        List<string> IGNORAR_PREFIJOS = new List<string> { "ADI", "OTR", "SPD" };
        List<int> ESTADOS_VALIDOS = new List<int> { 0, 1, 3 };
        List<int> ESTADO_FACTURA_INVALIDOS = new List<int> { 5, 6, 7 };

        List<JsonReducido> jsonReducidos = new List<JsonReducido>();

        foreach (var json in jsonCompleto)
        {
             if (!json.Estados.Any(e => e.EstadoComercializacion == 1) ||
                !json.Estados.Any(e => ESTADOS_VALIDOS.Contains(e.EstadoComercializacion)) ||
                json.Facturas.Any(f => f.EstadosFactura.Any(e => ESTADO_FACTURA_INVALIDOS.Contains(e.Estado))) ||
                !json.Facturas.Any() ||
                IGNORAR_PREFIJOS.Any(json.CodigoCotizacion.StartsWith))
            {
                continue;
            }
            var fechasEstados = json.Estados
                .Where(e => ESTADOS_VALIDOS.Contains(e.EstadoComercializacion))
                .Select(e => convertirFecha(e.Fecha))
                .OrderBy(f => f)
                .ToList();
            var estados = json.Estados
                .Where(e => ESTADOS_VALIDOS.Contains(e.EstadoComercializacion))
                .OrderBy(e => convertirFecha(e.Fecha))
                .Select(e => e.EstadoComercializacion)
                .ToList();
            var fechasFacturasPadre = json.Facturas
                .Select(f => convertirFecha(f.FechaFacturacion))
                .OrderBy(f => f)
                .ToList();
            var fechasFacturas = json.Facturas
                .SelectMany(f => f.EstadosFactura)
                .Select(e => convertirFecha(e.Fecha))
                .OrderBy(f => f)
                .ToList();
            var jsonReducido = JsonCompletoReductor.ReducirJson(
                json,
                estados.FirstOrDefault(), estados.LastOrDefault(),
                fechasEstados.FirstOrDefault() < convertirFecha(json.FechaInicio) ? fechasEstados.FirstOrDefault() : convertirFecha(json.FechaInicio), fechasEstados.LastOrDefault(),
                fechasFacturas.FirstOrDefault() < fechasFacturasPadre.FirstOrDefault() ? fechasFacturas.FirstOrDefault() : fechasFacturasPadre.FirstOrDefault(), fechasFacturas.LastOrDefault()
            );
            jsonReducidos.Add(JsonClienteReductor.Reducir(jsonReducido, jsonReducido.NombreCliente));
            jsonReducidos.Add(JsonClienteReductor.Reducir(jsonReducido, jsonReducido.LiderComercial));
        }
        return await Task.FromResult(jsonReducidos);
    }
    public static DateOnly convertirFecha(string fecha)
    {
        return DateOnly.ParseExact(fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    }
}
