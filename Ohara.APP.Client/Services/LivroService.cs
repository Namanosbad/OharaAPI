using Ohara.API.Shared.Enums;
using Ohara.API.Shared.Requests;
using Ohara.API.Shared.Responses;
using System.Net.Http.Json;

namespace Ohara.APP.Client.Services;

public class LivroService
{
    private readonly HttpClient _http;

    public LivroService(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("API");
    }

    // 👉 LISTAR TODOS
    public async Task<List<LivroResponse>> ObterTodos()
    {
        return await _http.GetFromJsonAsync<List<LivroResponse>>(
            "api/v1/livros"
        ) ?? new();
    }

    // 👉 BUSCAR POR ID
    public async Task<LivroResponse?> ObterPorId(Guid id)
    {
        return await _http.GetFromJsonAsync<LivroResponse>(
            $"api/v1/livros/{id}"
        );
    }

    // 👉 BUSCAR POR TÍTULO
    public async Task<List<LivroResponse>> BuscarPorTitulo(string titulo)
    {
        return await _http.GetFromJsonAsync<List<LivroResponse>>(
            $"api/v1/livros/titulo/{titulo}"
        ) ?? new();
    }

    // 👉 BUSCAR POR GÊNERO
    public async Task<List<LivroResponse>> BuscarPorGenero(EGenero genero)
    {
        return await _http.GetFromJsonAsync<List<LivroResponse>>(
            $"api/v1/livros/genero/{(int)genero}"
        ) ?? new();
    }

    // 👉 ADICIONAR LIVRO
    public async Task<LivroResponse?> Adicionar(LivroRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/v1/livros", request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Erro ao adicionar livro: {response.StatusCode} - {error}");
            return null;
        }

        return await response.Content.ReadFromJsonAsync<LivroResponse>();
    }

    // 👉 ATUALIZAR LIVRO
    public async Task<LivroResponse?> Atualizar(Guid id, LivroRequest request)
    {
        var response = await _http.PutAsJsonAsync($"api/v1/livros/{id}", request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Erro ao atualizar livro: {response.StatusCode} - {error}");
            return null;
        }

        return await response.Content.ReadFromJsonAsync<LivroResponse>();
    }

    // 👉 DELETAR LIVRO
    public async Task<bool> Deletar(Guid id)
    {
        var response = await _http.DeleteAsync($"api/v1/livros/{id}");

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Erro ao deletar livro: {response.StatusCode} - {error}");
        }

        return response.IsSuccessStatusCode;
    }
}
