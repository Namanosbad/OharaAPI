using Microsoft.AspNetCore.Mvc;
using Ohara.API.Application.Interfaces;
using Ohara.API.Shared.Enums;
using Ohara.API.Shared.Requests;

namespace Ohara.API.Internal.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class LivrosController : ControllerBase
    {
        private readonly ILivroService _livroService;
        public LivrosController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> BuscarLivro(Guid id)
        {
            var livros = await _livroService.BuscarLivroAsync(id);

            return Ok(livros);
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodosLivros()
        {
            var todos = await _livroService.BuscarTodosLivrosAsync();
            if (null == todos) return NotFound("Nenhum livro encontrado");
            return Ok(todos);
        }

        [HttpGet("titulo")]
        public async Task<IActionResult> BuscarLivroPorTitulo(string titulo)
        {
            var pelotitulo = await _livroService.BuscarPorTituloAsync(titulo);
            if (null == pelotitulo) return NotFound("Nenhum livro encontrado com este título");
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
            if (adicionarLivroRequest == null)
                return BadRequest("Os dados do livro são obrigatórios.");

            var livro = await _livroService.AdicionarLivroAsync(adicionarLivroRequest);

            if (livro == null)
                return BadRequest("Não foi possível cadastrar o livro. Verifique os dados enviados.");
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
            if (delete is false) return NotFound("Livro não encontrado para deletar");
            return Ok(delete);
        }
    }
}