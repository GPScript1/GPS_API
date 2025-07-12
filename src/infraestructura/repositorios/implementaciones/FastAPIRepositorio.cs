using System.Text;
using System.Text.Json;
using GPScript.NET.src.aplicaciones.DTOs.fastAPI;
using GPScript.NET.src.aplicaciones.DTOs.promedioSujeto;
using GPScript.NET.src.infraestructura.repositorios.interfaces;

namespace GPScript.NET.src.infraestructura.repositorios.implementaciones;

public class FastAPIRepositorio : IFastAPIRepositorio
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _apiKeyName;

    public FastAPIRepositorio(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _apiKey = Environment.GetEnvironmentVariable("FASTAPI_API_KEY") ?? string.Empty;
        _apiKeyName = Environment.GetEnvironmentVariable("FASTAPI_API_KEY_NAME") ?? string.Empty;
    }

    public async Task<IEnumerable<ClasificadorRespuesta>> EnviarDatosAsync(IEnumerable<PromedioSujeto> jsonData)
    {
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("api-key", _apiKey);
        _httpClient.DefaultRequestHeaders.Add("api-key-name", _apiKeyName);
        List<PromedioSujeto> dataList = jsonData.ToList();

        var json = JsonSerializer.Serialize(dataList);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(Environment.GetEnvironmentVariable("FASTAPI_URL") + "cluster", content);

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ClasificadorRespuesta>>(responseBody);
        }

        throw new Exception($"Error al enviar datos: {response.ReasonPhrase}");
    }
    public async Task<string> EntrenarModeloAsync(EntrenarModelo jsonData)
    {
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("api-key", _apiKey);
        _httpClient.DefaultRequestHeaders.Add("api-key-name", _apiKeyName);
        var json = JsonSerializer.Serialize(jsonData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(Environment.GetEnvironmentVariable("FASTAPI_URL") + "train", content);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }

        throw new Exception($"Error al entrenar modelo: {response.ReasonPhrase}");

    }
}