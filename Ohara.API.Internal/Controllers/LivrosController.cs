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

        [HttpGet ("{id:guid}")]
        public async Task<IActionResult> BuscarLivro(Guid id)
        {
            var livros = await _livroService.BuscarLivroAsync(id);
            return Ok(livros);
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodosLivros()
        {
            var todos = await _livroService.BuscarTodosLivrosAsync();
            return Ok(todos);
        }

        [HttpGet("titulo")]
        public async Task<IActionResult> BuscarLivroPorTitulo(string titulo)
        {
            var pelotitulo = await _livroService.BuscarPorTituloAsync(titulo);
            return Ok(pelotitulo);
        }

        [HttpGet("genero")]
        public async Task<IActionResult> BuscarPorGenero(EGenero genero)
        {
            var livros = await _livroService.LivroPorGeneroAsync(genero);

            if (livros == null || !livros.Any())
                return NotFound("Nenhum livro encontrado para este gênero.");

            return Ok(livros);
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarLivro([FromBody] LivroRequest adicionarLivroRequest)
        {
            var livro = await _livroService.AdicionarLivroAsync(adicionarLivroRequest);
            return CreatedAtAction(nameof(BuscarLivro), new { id = livro.Id }, livro);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> AtualizarLivro(Guid id, [FromBody] LivroRequest atualizarLivroRequest)
        {
            var atualizado = await _livroService.AtualizarLivroAsync(id, atualizarLivroRequest);

            if (atualizado == null)
                return NotFound("Livro não encontrado.");

            return Ok(atualizado);
        }

            [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletarLivro(Guid id)
        {
            var delete = await _livroService.DeletarLivroAsync(id);
            return Ok(delete);
        }
    }
}