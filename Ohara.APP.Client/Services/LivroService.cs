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

    // 👉 LISTAR TODOS (corresponde a BuscarTodosLivrosAsync)
    public async Task<List<LivroResponse>> ObterTodos()
    {
        return await _http.GetFromJsonAsync<List<LivroResponse>>(
            "api/livros"
        ) ?? new();
    }

    // 👉 BUSCAR POR ID (corresponde a BuscarLivroAsync)
    public async Task<LivroResponse?> ObterPorId(Guid id)
    {
        return await _http.GetFromJsonAsync<LivroResponse>(
            $"api/livros/{id}"
        );
    }

    // 👉 BUSCAR POR TÍTULO (corresponde a BuscarPorTituloAsync)
    public async Task<List<LivroResponse>> BuscarPorTitulo(string titulo)
    {
        return await _http.GetFromJsonAsync<List<LivroResponse>>(
            $"api/livros/titulo/{titulo}"
        ) ?? new();
    }

    // 👉 BUSCAR POR GÊNERO (corresponde a LivroPorGeneroAsync)
    public async Task<List<LivroResponse>> BuscarPorGenero(EGenero genero)
    {
        return await _http.GetFromJsonAsync<List<LivroResponse>>(
            $"api/livros/genero/{(int)genero}"
        ) ?? new();
    }

    // 👉 ADICIONAR LIVRO (corresponde a AdicionarLivroAsync)
    public async Task<LivroResponse?> Adicionar(LivroRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/livros", request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<LivroResponse>();
    }

    // 👉 ATUALIZAR LIVRO (corresponde a AtualizarLivroAsync)
    public async Task<LivroResponse?> Atualizar(Guid id, LivroRequest request)
    {
        var response = await _http.PutAsJsonAsync($"api/livros/{id}", request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<LivroResponse>();
    }

    // 👉 DELETAR LIVRO (corresponde a DeletarLivroAsync)
    public async Task<bool> Deletar(Guid id)
    {
        var response = await _http.DeleteAsync($"api/livros/{id}");
        return response.IsSuccessStatusCode;
    }
}
