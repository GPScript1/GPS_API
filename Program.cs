using GPScript.NET.src.infraestructura.datos;
using Microsoft.EntityFrameworkCore;
using dotenv.net;
using dotenv.net.Utilities;
using GPScript.NET.src.aplicaciones.servicios.interfaces;
using GPScript.NET.src.aplicaciones.servicios.implementaciones;

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
            builder.WithOrigins("http://localhost:3000");
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
            builder.AllowCredentials();
        });
});

builder.Services.AddDbContext<ContextoDatos>(options => 
    options.UseNpgsql(EnvReader.GetStringValue("POSTGRESQL_CONNECTION")));

builder.Services.AddScoped<IDatoServicio, DatoServicio>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();