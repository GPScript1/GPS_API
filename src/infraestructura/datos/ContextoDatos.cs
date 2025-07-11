using Microsoft.EntityFrameworkCore;

namespace GPScript.NET.src.infraestructura.datos;

public class ContextoDatos(DbContextOptions<ContextoDatos> options) : DbContext(options)
{

    
    
}