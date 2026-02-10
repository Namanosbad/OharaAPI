using Ohara.API.Shared.Responses;
using System.Net.Http.Json;

namespace Ohara.APP.Client.Services;

public class AutorService
{
    private readonly HttpClient _http;

    public AutorService(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("API");
    }

    public async Task<List<AutorResponse>> ObterTodos()
    {
        return await _http.GetFromJsonAsync<List<AutorResponse>>(
            "api/v1/autor/listar"
        ) ?? new();
    }

    // 🔥 MÉTODO CORRETO (é aqui que estava o 404)
    public async Task<AutorResponse?> ObterPorId(Guid id)
    {
        return await _http.GetFromJsonAsync<AutorResponse>(
            $"api/v1/autor/{id}"
        );
    }
}
