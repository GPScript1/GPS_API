# API REST en .NET - Gestión de Comercializaciones

Este proyecto es una API REST desarrollada en .NET 9, diseñada para procesar datos de comercializaciones hacia una API de predicción en FastAPI. Incluye autenticación por API Key y manejo de modelos DTO para la integración de datos.

## Características

- API REST desarrollada con ASP.NET Core.
- Envío de datos a una API externa en FastAPI.
- Inyección de dependencias y configuración de `HttpClient`.
- Validación de datos de entrada.
- Configuración de variables de entorno para mayor seguridad.

## Tecnologías utilizadas

- .NET 9
- ASP.NET Core Web API
- C#
- System.Text.Json
- HttpClient
- Variables de entorno con `Environment.GetEnvironmentVariable`

## Configuración

```json
"environmentVariables": {
  "FASTAPI_URL": "TuAPIUrl",
  "FASTAPI_API_KEY": "tu_api_key",
  "FASTAPI_API_KEY_NAME": "api-key"
}
```

```
FASTAPI_URL=TuAPIUrl
FASTAPI_API_KEY=tu_api_key
FASTAPI_API_KEY_NAME=api-key
```

## Ejecución del proyecto
```bash
git clone https://github.com/GPScript1/GPS_API
cd GPS_API 
```

```bash
dotnet restore
dotnet build
dotnet run
```

## Pruebas

Puedes usar Postman o cURL para probar el endpoint.