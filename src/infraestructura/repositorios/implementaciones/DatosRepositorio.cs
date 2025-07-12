using GPScript.NET.src.aplicaciones.DTOs.fastAPI;
using GPScript.NET.src.dominio.modelos;
using GPScript.NET.src.infraestructura.datos;
using GPScript.NET.src.infraestructura.repositorios.interfaces;

namespace GPScript.NET.src.infraestructura.repositorios.implementaciones;

public class DatosRepositorio : IDatosRepositorio
{
    private readonly ContextoDatos _contexto;

    public DatosRepositorio(ContextoDatos contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> GuardarDatosAsync(IEnumerable<GuardadoRespuestas> datos)
    {
        var datosGuardados = new List<GuardadoRespuestas>();
        foreach (var dato in datos)
        {
            var entidad = new DatosDePredicciones
            {
                NombreEnte = dato.NombreEnte,
                PromedioInicioComFinCom = dato.PromedioInicioComFinCom,
                PromedioFinComInicioFactura = dato.PromedioFinComInicioFactura,
                PromedioInicioFacturaFinPagado = dato.PromedioInicioFacturaFinPagado,
                PromedioInicioComFinPagado = dato.PromedioInicioComFinPagado,
                CategoriaRiesgo = dato.CategoriaRiesgo,
                DiasDemoraRealPromedio = dato.DiasDemoraRealPromedio,
                DiasDemoraPredicho = dato.DiasDemoraPredicho,
                DiferenciaDias = dato.DiferenciaDias
            };
            _contexto.DatosDePredicciones.Add(entidad);
            datosGuardados.Add(dato);
        }
        await _contexto.SaveChangesAsync();
        return true;
    }
    public async Task<IQueryable<GuardadoRespuestas>> ObtenerDatosAsync(string? ente, string? categoria, int? cantidad = 20, int? pagina = 1)
    {
        var categoriasPermitidas = new[] { "riesgo bajo", "riesgo medio", "riesgo alto" };
        if ((cantidad.HasValue && cantidad.Value <= 0) || (cantidad.HasValue && cantidad.Value > 50))
        {
            cantidad = 20;
        }
        if (pagina.HasValue && pagina.Value <= 0)
        {
            pagina = 1;
        }
        var query = _contexto.DatosDePredicciones.AsQueryable();

        if (!string.IsNullOrEmpty(ente))
        {
            query = query.Where(d => d.NombreEnte.ToLower().Contains(ente.ToLower()));
        }

        if (cantidad.HasValue && pagina.HasValue)
        {
            query = query.Skip((pagina.Value - 1) * cantidad.Value).Take(cantidad.Value);
        }
        if (!string.IsNullOrEmpty(categoria) && categoriasPermitidas.Contains(categoria.ToLower()))
        {
            query = query.Where(d => d.CategoriaRiesgo.ToLower() == categoria.ToLower()).OrderByDescending(d => d.DiasDemoraRealPromedio);
        }
        return await Task.FromResult(query.Select(d => new GuardadoRespuestas
        {
            NombreEnte = d.NombreEnte,
            PromedioInicioComFinCom = d.PromedioInicioComFinCom,
            PromedioFinComInicioFactura = d.PromedioFinComInicioFactura,
            PromedioInicioFacturaFinPagado = d.PromedioInicioFacturaFinPagado,
            PromedioInicioComFinPagado = d.PromedioInicioComFinPagado,
            CategoriaRiesgo = d.CategoriaRiesgo,
            DiasDemoraRealPromedio = d.DiasDemoraRealPromedio,
            DiasDemoraPredicho = d.DiasDemoraPredicho,
            DiferenciaDias = d.DiferenciaDias
        }));
    }
}
