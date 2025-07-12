using GPScript.NET.src.infraestructura.datos;
using Microsoft.EntityFrameworkCore;
using dotenv.net;
using dotenv.net.Utilities;
using GPScript.NET.src.aplicaciones.servicios.interfaces;
using GPScript.NET.src.aplicaciones.servicios.implementaciones;
using GPScript.NET.src.infraestructura.repositorios.implementaciones;
using GPScript.NET.src.infraestructura.repositorios.interfaces;

DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins(EnvReader.GetStringValue("CORS_ORIGIN"));
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
            builder.AllowCredentials();
        });
});

builder.Services.AddDbContext<ContextoDatos>(options => 
    options.UseNpgsql(EnvReader.GetStringValue("POSTGRESQL_CONNECTION")));

builder.Services.AddScoped<IDatoServicio, DatoServicio>();
builder.Services.AddScoped<IPrediccionServicio, PrediccionServicio>();
builder.Services.AddHttpClient<IFastAPIRepositorio, FastAPIRepositorio>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();
app.Run();