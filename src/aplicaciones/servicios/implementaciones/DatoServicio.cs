using System.Globalization;
using System.Linq;
using GPScript.NET.src.aplicaciones.DTOs.fastAPI;
using GPScript.NET.src.aplicaciones.DTOs.jsonDTOs;
using GPScript.NET.src.aplicaciones.DTOs.promedioSujeto;
using GPScript.NET.src.aplicaciones.mapeadores.jsonMapeadores;
using GPScript.NET.src.aplicaciones.mapeadores.sujetoMapeadores;
using GPScript.NET.src.aplicaciones.servicios.interfaces;
using GPScript.NET.src.infraestructura.repositorios.interfaces;

namespace GPScript.NET.src.aplicaciones.servicios.implementaciones;

public class DatoServicio : IDatoServicio
{
    private readonly IFastAPIRepositorio _fastAPIRepositorio;
    public DatoServicio(IFastAPIRepositorio fastAPIRepositorio)
    {
        _fastAPIRepositorio = fastAPIRepositorio;
    }
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
                fechasFacturas.FirstOrDefault() < fechasFacturasPadre.FirstOrDefault() ? fechasFacturas.FirstOrDefault() : fechasFacturasPadre.FirstOrDefault(), fechasFacturas.LastOrDefault());

            var jsonReducidoConValor = JsonClienteReductor.Reducir(jsonReducido, jsonReducido.NombreCliente);
            jsonReducidoConValor.ValorComercializacion = json.ValorFinalComercializacion;
            jsonReducidos.Add(jsonReducidoConValor);
            jsonReducidos.Add(JsonClienteReductor.Reducir(jsonReducido, jsonReducido.LiderComercial));
        }
        return await Task.FromResult(jsonReducidos);
    }
    public async Task<IEnumerable<PromedioSujeto>> CalcularPromedioSujetos(JsonCompleto[] jsonCompleto)
    {
        List<string> IGNORAR_PREFIJOS = new List<string> { "ADI", "OTR", "SPD" };
        List<int> ESTADOS_VALIDOS = new List<int> { 0, 1, 3 };
        List<int> ESTADO_FACTURA_INVALIDOS = new List<int> { 5, 6, 7 };

        List<JsonReducido> jsonReducidos = new List<JsonReducido>();
        List<ClienteValorCantidad> clienteValorCantidades = new List<ClienteValorCantidad>();

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
                fechasFacturas.FirstOrDefault() < fechasFacturasPadre.FirstOrDefault() ? fechasFacturas.FirstOrDefault() : fechasFacturasPadre.FirstOrDefault(), fechasFacturas.LastOrDefault());

            clienteValorCantidades.Add(new ClienteValorCantidad { Nombre = jsonReducido.NombreCliente, ValorComercializacion = jsonReducido.ValorFinal });
            clienteValorCantidades.Add(new ClienteValorCantidad { Nombre = jsonReducido.LiderComercial, ValorComercializacion = 0 });

            jsonReducidos.Add(JsonClienteReductor.Reducir(jsonReducido, jsonReducido.NombreCliente));
            jsonReducidos.Add(JsonClienteReductor.Reducir(jsonReducido, jsonReducido.LiderComercial));
        }

        var promedios = new List<PromedioSujeto>();
        var entesSinRepetir = jsonReducidos
            .GroupBy(j => j.NombreEnte)
            .Select(g => g.First())
            .ToList();
        foreach (var ente in entesSinRepetir)
        {
            double promedioInicioComFinCom = jsonReducidos
                .Where(j => j.NombreEnte == ente.NombreEnte)
                .Average(j => j.DiasInicioComFinCom);
            double promedioFinComInicioFactura = jsonReducidos
                .Where(j => j.NombreEnte == ente.NombreEnte)
                .Average(j => j.DiasFinComInicioFactura);
            double promedioInicioFacturaFinPagado = jsonReducidos
                .Where(j => j.NombreEnte == ente.NombreEnte)
                .Average(j => j.DiasInicioFacturaFinPagado);
            double promedioInicioComFinPagado = jsonReducidos
                .Where(j => j.NombreEnte == ente.NombreEnte)
                .Average(j => j.DiasInicioComFinPagado);
            int valorTotalAcumulado = clienteValorCantidades
                .Where(c => c.Nombre == ente.NombreEnte)
                .Sum(c => c.ValorComercializacion);
            int cantidadDeComercializaciones = clienteValorCantidades
                .Where(c => c.Nombre == ente.NombreEnte)
                .Count();
            promedios.Add(SujetoMapeador.MapearSujeto(
                ente,
                (int)promedioInicioComFinCom,
                (int)promedioFinComInicioFactura,
                (int)promedioInicioFacturaFinPagado,
                (int)promedioInicioComFinPagado,
                valorTotalAcumulado,
                cantidadDeComercializaciones
            ));
        }
        return await Task.FromResult(promedios);
    }
    public async Task<IEnumerable<ClasificadorRespuesta>> EnviarDatosAsync(IEnumerable<PromedioSujeto> jsonData)
    {
        if (jsonData == null || !jsonData.Any())
        {
            throw new ArgumentException("Los datos a enviar no pueden ser nulos o vacíos.");
        }
        var resultado = await _fastAPIRepositorio.EnviarDatosAsync(jsonData);
        if (resultado == null)
        {
            throw new Exception("Error al enviar los datos al repositorio.");
        }
        return resultado;
    }
    public async Task<IEnumerable<PrediccionesRespuesta>> EntrenarModeloAsync(JsonCompleto[] jsonCompleto)
    {
        List<string> IGNORAR_PREFIJOS = new List<string> { "ADI", "OTR", "SPD" };
        List<int> ESTADOS_VALIDOS = new List<int> { 0, 1, 3 };
        List<int> ESTADO_FACTURA_INVALIDOS = new List<int> { 5, 6, 7 };

        List<JsonReducido> jsonReducidos = new List<JsonReducido>();
        List<ClienteValorCantidad> clienteValorCantidades = new List<ClienteValorCantidad>();

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
                fechasFacturas.FirstOrDefault() < fechasFacturasPadre.FirstOrDefault() ? fechasFacturas.FirstOrDefault() : fechasFacturasPadre.FirstOrDefault(), fechasFacturas.LastOrDefault());

            clienteValorCantidades.Add(new ClienteValorCantidad { Nombre = jsonReducido.NombreCliente, ValorComercializacion = jsonReducido.ValorFinal });
            clienteValorCantidades.Add(new ClienteValorCantidad { Nombre = jsonReducido.LiderComercial, ValorComercializacion = 0 });

            jsonReducidos.Add(JsonClienteReductor.Reducir(jsonReducido, jsonReducido.NombreCliente));
            jsonReducidos.Add(JsonClienteReductor.Reducir(jsonReducido, jsonReducido.LiderComercial));
        }

        var promedios = new List<PromedioSujeto>();
        var entesSinRepetir = jsonReducidos
            .GroupBy(j => j.NombreEnte)
            .Select(g => g.First())
            .ToList();
        foreach (var ente in entesSinRepetir)
        {
            double promedioInicioComFinCom = jsonReducidos
                .Where(j => j.NombreEnte == ente.NombreEnte)
                .Average(j => j.DiasInicioComFinCom);
            double promedioFinComInicioFactura = jsonReducidos
                .Where(j => j.NombreEnte == ente.NombreEnte)
                .Average(j => j.DiasFinComInicioFactura);
            double promedioInicioFacturaFinPagado = jsonReducidos
                .Where(j => j.NombreEnte == ente.NombreEnte)
                .Average(j => j.DiasInicioFacturaFinPagado);
            double promedioInicioComFinPagado = jsonReducidos
                .Where(j => j.NombreEnte == ente.NombreEnte)
                .Average(j => j.DiasInicioComFinPagado);
            int valorTotalAcumulado = clienteValorCantidades
                .Where(c => c.Nombre == ente.NombreEnte)
                .Sum(c => c.ValorComercializacion);
            int cantidadDeComercializaciones = clienteValorCantidades
                .Where(c => c.Nombre == ente.NombreEnte)
                .Count();
            promedios.Add(SujetoMapeador.MapearSujeto(
                ente,
                (int)promedioInicioComFinCom,
                (int)promedioFinComInicioFactura,
                (int)promedioInicioFacturaFinPagado,
                (int)promedioInicioComFinPagado,
                valorTotalAcumulado,
                cantidadDeComercializaciones
            ));
        }
        var entrenarModelo = new EntrenarModelo
        {
            ClientePromedio = promedios,
            Comercializaciones = jsonReducidos
        };
        var resultado = await _fastAPIRepositorio.EntrenarModeloAsync(entrenarModelo);
        if (resultado == null || !resultado.Any())
        {
            throw new Exception("Error al entrenar el modelo.");
        }
        return resultado;
    }
    public static DateOnly convertirFecha(string fecha)
    {
        return DateOnly.ParseExact(fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    }
}
