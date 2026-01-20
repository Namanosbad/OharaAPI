using Microsoft.AspNetCore.Mvc;
using Ohara.API.Application.Interfaces;
using Ohara.API.Application.Requests;
using Ohara.API.Application.Responses;
using Ohara.API.Domain.Entities;
using Ohara.API.Domain.Enums;

namespace Ohara.API.Internal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly ILivroService _livroService;
        public LivrosController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        [HttpGet ("buscar-livro/{id:guid}")]
        public async Task<IActionResult> BuscarLivro(Guid id)
        {
            var livros = await _livroService.BuscarLivroAsync(id);
            return Ok(livros);
        }

        [HttpGet("buscar-todos-livros")]
        public async Task<IActionResult> BuscarTodosLivros()
        {
            var todos = await _livroService.BuscarTodosLivrosAsync();
            return Ok(todos);
        }

        [HttpGet("buscar-por-titulo")]
        public async Task<IActionResult> BuscarLivroPorTitulo(string titulo)
        {
            var pelotitulo = await _livroService.BuscarPorTituloAsync(titulo);
            return Ok(pelotitulo);
        }

        [HttpGet("buscar-genero")]
        public async Task<IActionResult> BuscarPorGenero(EGenero genero)
        {
            var buscagenero = await _livroService.LivroPorGenero(genero);
            return Ok(buscagenero);
        }

        [HttpPost("cadastrar-livro")]
        public async Task<AdicionarLivroResponse> CadastrarLivro(AdicionarLivroRequest adicionarLivroRequest)
        {
            var autor = await _livroService.AdicionarLivroAsync(adicionarLivroRequest);
            return autor;
        }

        [HttpPut("atualizar-livro")]
        public async Task<IActionResult> AtualizarLivro(Livro livro)
        {
            var atualizar = await _livroService.AtualizarLivroAsync(livro);
            return Ok(atualizar);
        }

        [HttpDelete("deletar-livro")]
        public async Task<IActionResult> DeletarLivro(Guid id)
        {
            var delete = await _livroService.DeletarLivroAsync(id);
            return Ok(delete);
        }
    }
}